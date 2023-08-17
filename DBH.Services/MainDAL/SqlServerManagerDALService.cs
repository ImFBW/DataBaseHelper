using DBH.DALProvider;
using DBH.DALProvider.MainDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DBH.Models.Entitys;
using System.Threading.Tasks;
using DBH.Models.EntityViews;
using DBH.Models.Common;
using DBH.Core.Dapper;
using DBH.Core;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Data;

namespace DBH.DALServices.MainDAL
{
    /// <summary>
    /// 关于配置的查询-sqlserver数据库类型
    /// </summary>
    public class SqlServerManagerDALService : BaseDALService, ISqlServerManagerDALProvider
    {
        /// <summary>
        /// 单独使用一个连接字符串
        /// </summary>
        private string ConnectionString;
        public SqlServerManagerDALService(IDBConnectionProvider dBConnectionProvider)
        {
            // if (string.IsNullOrEmpty(connectionstring)) throw new ArgumentException("缺少连接字符串参数");
            ConnectionProvider = dBConnectionProvider;
            //ConnectionProvider.ConnectionString = connectionstring;//配置连接字符串
            ConnectionProvider.DBCategory = DBCategory.SqlServer;
        }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SetConnectionString(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }


        /// <summary>
        /// 进行搜索-SQLServer数据库模式
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="top">默认查询的数量(合并查询，结果可能超出此值)，
        /// 太多没意义，分页没必要，可以通过精确搜索查出范围内的数据</param>
        /// <returns></returns>
        public async Task<IList<SysDataBaseSearchView>> SearchActionAsync(string searchText, int top = 100)
        {
            List<SysDataBaseSearchView> listView = new List<SysDataBaseSearchView>();
            using (var conn = ConnectionProvider.GetConnection(this.ConnectionString))
            {
                conn.Open();
                string sqlQuery = string.Empty;
                #region 第一步：搜索表名、存储过程名、表值函数名
                sqlQuery = string.Format($"SELECT TOP {top} obj.name AS TypeName,CASE obj.xtype WHEN 'U' THEN 1 WHEN 'P' THEN 2 WHEN 'TF' THEN 3 END AS DBObjectType FROM dbo.sysobjects obj WHERE  obj.xtype IN('U','P','TF') AND obj.name LIKE '%" + @searchText + "%'");
                var list1 = await conn.QueryAsync<SysDataBaseSearchView>(sqlQuery, new { searchText = searchText });
                if (list1 != null && list1.Count() > 0)
                {
                    listView.AddRange(list1.ToList());
                }
                #endregion

                #region 第二步：搜索表字段,返回表名
                sqlQuery = string.Format($"SELECT DISTINCT TOP {top} 4 AS DBObjectType,obj.name AS TypeName FROM dbo.syscolumns col INNER JOIN dbo.sysobjects obj ON col.id = obj.id  AND obj.xtype = 'U' AND obj.status >= 0 WHERE col.name LIKE'%" + @searchText + "%'");
                var list2 = await conn.QueryAsync<SysDataBaseSearchView>(sqlQuery, new { searchText = searchText });
                if (list2 != null && list2.Count() > 0)
                {
                    foreach (var item in list2)
                    {
                        if (listView.Count(p => p.TypeName == item.TypeName && p.DBObjectType == DBObjectType.U) == 0)
                        {
                            listView.Add(item);
                        }
                    }
                }
                #endregion

            }
            return listView;
        }


        /// <summary>
        /// 查询存储过程、表值函数，返回结果集(以行为单位显示)
        /// </summary>
        /// <param name="dbTypeName">存储过程名或函数名</param>
        /// <returns></returns>
        public async Task<IList<Definition>> GetDefinitionsAsync(string dbTypeName)
        {
            IList<Definition> listDefinition = new List<Definition>();
            using (var conn = ConnectionProvider.GetConnection(this.ConnectionString))
            {
                DataSet ds = await conn.ExecuteTableAsync("sp_helptext", new { objname = dbTypeName }, CommandType.StoredProcedure);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    listDefinition = DataHelper.DataTableToList<Definition>(ds.Tables[0]);
                }
            }
            return listDefinition;
        }

        /// <summary>
        /// 查询表的全部列，返回List
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IList<DB_TableColumnsView>> GetTableColumnsListAsync(string tableName)
        {
            IList<DB_TableColumnsView> listView = new List<DB_TableColumnsView>();
            using (var conn = ConnectionProvider.GetConnection(this.ConnectionString))
            {
                string querySql = string.Format(@"SELECT  
        col.colorder AS RowNumber ,obj.name AS TableName,ISNULL(eptable.value,'') AS TableDesc,
        col.name AS ColumnName ,
        ISNULL(ep.[value], '') AS ColumnDesc ,
        t.name AS ColumnDataType ,
        col.length AS ColumnDataLength ,
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN 1
             ELSE 0
        END AS IsIdentity ,
        CASE WHEN EXISTS ( SELECT   1
                           FROM     dbo.sysindexes si
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                                                              AND si.indid = sik.indid
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                                                              AND sc.colid = sik.colid
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name
                                                              AND so.xtype = 'PK'
                           WHERE    sc.id = col.id
                                    AND sc.colid = col.colid ) THEN 1
             ELSE 0
        END AS IsKey,
        CASE WHEN col.isnullable = 1 THEN 1  ELSE 0  END AS [IsNullable],
        ISNULL(comm.text, '') AS DefaultValue
FROM    dbo.syscolumns col
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                         AND obj.xtype = 'U'
                                         AND obj.status >= 0
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
        LEFT  JOIN sys.extended_properties eptable ON obj.id = eptable.major_id
                                                         AND eptable.minor_id = 0
                                                         AND eptable.name = 'MS_Description'
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                      AND col.colid = ep.minor_id
                                                      AND ep.name = 'MS_Description'
WHERE   obj.name = @tableName--表名
ORDER BY col.colorder ; ");
                var resData = await conn.QueryAsync<DB_TableColumnsView>(querySql, new { tableName = tableName });
                if (resData != null && resData.Count() > 0)
                {
                    listView = resData.ToList();
                }
            }
            return listView;
        }

        /// <summary>
        /// 更新表、字段的说明
        /// </summary>
        /// <param name="tableColumnDescription">数据实体类</param>
        /// <returns></returns>
        public async Task<EntityResult> UpdateTableColumnDescriptionAsync(TableColumnDescription tableColumnDescription)
        {
            EntityResult result = new EntityResult();
            using (var conn = ConnectionProvider.GetConnection(this.ConnectionString))
            {
                string sql = string.Empty;
                if (tableColumnDescription.TypeID == 1)
                {
                    sql = string.Format(@"IF EXISTS(SELECT 1 FROM    dbo.syscolumns col
                                        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
                                        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                         AND obj.xtype = 'U'
                                                                         AND obj.status >= 0
                                        LEFT  JOIN sys.extended_properties eptable ON obj.id = eptable.major_id
                                                                                         AND eptable.minor_id = 0
                                                                                         AND eptable.name = 'MS_Description'
                                        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                      AND col.colid = ep.minor_id
                                                      AND ep.name = 'MS_Description' WHERE obj.name = @TableName AND col.name=@TableColumn  AND eptable.major_id IS NOT null)
                            BEGIN
	                            --更新说明
                            EXEC sys.sp_updateextendedproperty @name = 'MS_Description',     -- sysname
                                                            @value = @Description,  -- sql_variant
                                                            @level0type = 'schema',       -- varchar(128)
                                                            @level0name = N'dbo',         -- sysname
                                                            @level1type = N'table',       -- varchar(128)
                                                            @level1name = @TableName -- sysname
								SELECT 1
                            END 
                            ELSE
                            BEGIN
                            --新增说明
                             EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                                            @value = @Description,  -- sql_variant
                                                            @level0type = 'schema',       -- varchar(128)
                                                            @level0name = N'dbo',         -- sysname
                                                            @level1type = N'table',       -- varchar(128)
                                                            @level1name = @TableName -- sysname
								SELECT 1
                            END");
                }
                else if (tableColumnDescription.TypeID == 2)
                {
                    sql = string.Format(@"IF EXISTS(SELECT 1 FROM    dbo.syscolumns col
                                        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
                                        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                         AND obj.xtype = 'U'
                                                                         AND obj.status >= 0
                                        LEFT  JOIN sys.extended_properties eptable ON obj.id = eptable.major_id
                                                                                         AND eptable.minor_id = 0
                                                                                         AND eptable.name = 'MS_Description'
                                        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                      AND col.colid = ep.minor_id
                                                      AND ep.name = 'MS_Description' WHERE   obj.name = @TableName AND col.name=@TableColumn AND ep.major_id IS NOT NULL)
                            BEGIN
	                            --更新说明
                            EXEC sys.sp_updateextendedproperty @name = 'MS_Description',     -- sysname
                                                            @value = @Description,  -- sql_variant
                                                            @level0type = 'schema',       -- varchar(128)
                                                            @level0name = N'dbo',         -- sysname
                                                            @level1type = N'table',       -- varchar(128)
                                                            @level1name = @TableName, -- sysname
                                                            @level2type = N'column',      -- varchar(128)
                                                            @level2name = @TableColumn;
								SELECT 1
                            END 
                            ELSE
                            BEGIN
                            --新增说明
                             EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                                            @value = @Description,  -- sql_variant
                                                            @level0type = 'schema',       -- varchar(128)
                                                            @level0name = N'dbo',         -- sysname
                                                            @level1type = N'table',       -- varchar(128)
                                                            @level1name = @TableName, -- sysname
                                                            @level2type = N'column',      -- varchar(128)
                                                            @level2name = @TableColumn;
								SELECT 1
                            END");
                }
                object code = await conn.ExecuteScalarAsync(sql, new { TableName = tableColumnDescription.TableName, TableColumn = tableColumnDescription.TableColumn, Description = tableColumnDescription.Description });
                if (code != null && int.Parse(code.ToString()) > 0)
                {
                    result.EntityCode = EntityCode.Success;
                    result.Message = "success";
                }
                else
                {
                    result.EntityCode = EntityCode.Fail;
                    result.Message = "fail";
                }
            }
            return result;
        }
        //public async Task<EntityResult>
    }
}
