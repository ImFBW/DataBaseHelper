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
        public async Task<IList<SysDataBaseSearchView>> SearchAction(string searchText, int top = 100)
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

                #region 第二步：搜索表字段
                sqlQuery = string.Format($"SELECT DISTINCT TOP {top} 1 AS DBObjectType,obj.name AS TypeName FROM dbo.syscolumns col INNER JOIN dbo.sysobjects obj ON col.id = obj.id  AND obj.xtype = 'U' AND obj.status >= 0 WHERE col.name LIKE'%" + @searchText + "%'");
                var list2 = await conn.QueryAsync<SysDataBaseSearchView>(sqlQuery, new { searchText = searchText });
                if (list2 != null && list2.Count() > 0)
                {
                    foreach (var item in list2)
                    {
                        if (listView.Count(p => p.TypeName == item.TypeName && p.DBObjectType == item.DBObjectType) == 0)
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

    }
}
