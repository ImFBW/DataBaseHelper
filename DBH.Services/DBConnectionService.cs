using DBH.Core;
using DBH.DALProvider;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALServices
{
    public class DBConnectionService : IDBConnectionProvider
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBCategory DBCategory { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        ///// <summary>
        ///// 构造函数
        ///// 直接读取配置的连接字符串
        ///// </summary>
        ///// <param name="configuration"></param>
        //public DBConnectionService(IConfiguration configuration)
        //{
        //    this.ConnectionString = configuration.GetConnectionString(ConnectionString);
        //}
        /// <summary>
        /// 根据不同数据库类型，返回对应的数据库连接类型
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                throw new ArgumentNullException(nameof(this.ConnectionString));
            }
            IDbConnection connection;
            switch (DBCategory)
            {
                case DBCategory.SqlServer:
                    connection = new SqlConnection(this.ConnectionString);
                    break;
                case DBCategory.MySql:
                    connection = new MySqlConnection(this.ConnectionString);
                    break;
                default:
                    connection = new SqlConnection(this.ConnectionString);
                    break;
            }
            return connection;
        }
    }
}
