using DBH.Core;
using DBH.DALProvider;
using DBH.DALProvider.MainDAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALServices.MainDAL
{
    /// <summary>
    /// 关于配置的查询-MySql数据库类型
    /// </summary>
    public class MySqlManagerDALService : BaseDALService, IMySqlManagerDALProvider
    {
        public MySqlManagerDALService(IConfiguration configuretion, IDBConnectionProvider dBConnectionProvider)
        {
            ConnectionProvider = dBConnectionProvider;
            ConnectionProvider.ConnectionString = configuretion.GetConnectionString("DBMSToolConnection").ToString();//配置连接字符串
            ConnectionProvider.DBCategory = DBCategory.MySql;
        }



    }
}
