using DBH.Core;
using DBH.Core.Setting;
using DBH.DALProvider;
using DBH.DALProvider.MainDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;
using DBH.Models.Common;
using DBH.Models.EntityViews;

namespace DBH.DALServices.MainDAL
{
    /// <summary>
    /// 关于配置的查询-MySql数据库类型
    /// </summary>
    public class MySqlManagerDALService : BaseDALService, IMySqlManagerDALProvider
    {
        private string ConnectionString;

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SetConnectionString(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException(nameof(this.ConnectionString));
            }
            IDbConnection connection = new MySqlConnection(ConnectionString);
            return connection;
        }

        /// <summary>
        /// 测试一个连接字符串是否可以打开连接成功
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public bool TestConnection(string connectionString)
        {
            bool isConn = false;
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    isConn = true;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                isConn = false;
                throw ex;
            }
            return isConn;
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
            using (var conn = GetConnection())
            {
                conn.Open();
                string sqlQuery = string.Empty;
                #region 第一步：搜索表名、存储过程名、函数名（目前仅支持表名）
                sqlQuery = string.Format($"SELECT T1.table_name AS TypeName,'U' DBObjectType FROM INFORMATION_SCHEMA.TABLES T1 inner join (select DATABASE() AS dbname) AS T2 on T1.table_schema=T2.dbname where T1.table_name like '%" + @searchText + $"%' limit {top}");
                var list1 = await conn.QueryAsync<SysDataBaseSearchView>(sqlQuery, new { searchText = searchText });
                if (list1 != null && list1.Count() > 0)
                {
                    listView.AddRange(list1.ToList());
                }
                #endregion

                #region 第二步：搜索表字段,返回表名
                sqlQuery = string.Format($"SELECT DISTINCT T1.table_name AS TypeName,'U' DBObjectType FROM INFORMATION_SCHEMA.COLUMNS T1 inner join (select DATABASE() AS dbname) AS T2 on T1.table_schema=T2.dbname where T1.column_name like '%" + @searchText + $"%' limit {top}");
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
        /// 查询表的全部列，返回List
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IList<DB_TableColumnsView>> GetTableColumnsListAsync(string tableName)
        {
            IList<DB_TableColumnsView> listView = new List<DB_TableColumnsView>();
            using (var conn = GetConnection())
            {
                string querySql = string.Format(@"select row_number() over(order by columnName) RowNumber, T1.*,T2.ColumnName,T2.ColumnDesc,T2.ColumnDataType,T2.ColumnDataLength,T2.IsNullable,T2.IsKey,T2.IsIdentity,T2.DefaultValue
 from (
select table_name AS TableName,table_comment AS TableDesc from INFORMATION_SCHEMA.TABLES where table_name=@tableName
) AS T1
inner join 
(
select table_name AS TableName,column_name AS ColumnName,column_comment AS ColumnDesc,column_type AS ColumnDataType,IFNULL(CHARACTER_MAXIMUM_LENGTH,0) AS ColumnDataLength,case is_NULLABLE when 'Yes' then 1 else 0 END AS IsNullable,case COLUMN_key when 'PRI' then 1 else 0 END AS IsKey,case EXTRA when 'auto_increment' then 1 else 0 END IsIdentity,IFNULL(column_default,'') AS DefaultValue from INFORMATION_SCHEMA.COLUMNS where table_name=@tableName
) AS T2 on T1.TableName=T2.TableName");
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
            using (var conn = GetConnection())
            {
                string sql = string.Empty;
                if (tableColumnDescription.TypeID == 1)
                {
                    sql = string.Format(@$"alter table {tableColumnDescription.TableName} comment @Description;select 1");
                }
                else if (tableColumnDescription.TypeID == 2)
                {
                    //MySql修改表字段说明，需要用到字段的数据类型，因此需要2步：
                    //第一步：查出字段类型
                    sql = string.Format("select column_type from information_schema.`COLUMNS` where table_name=@TableName and column_name=@TableColumn");
                    string column_type = conn.QueryFirst<string>(sql, new { TableName = tableColumnDescription.TableName, TableColumn = tableColumnDescription.TableColumn, });

                    //第二步：拼接Sql修改字段说明
                    sql = string.Format($"alter table {tableColumnDescription.TableName} MODIFY {tableColumnDescription.TableColumn} {column_type} comment @Description;select 1");
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

    }
}
