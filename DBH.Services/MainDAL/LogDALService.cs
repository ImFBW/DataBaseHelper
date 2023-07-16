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
    public class LogDALService:BaseDALService, ILogDALProvider
    {
        public LogDALService(IConfiguration configuretion, IDBConnectionProvider dBConnectionProvider)
        {
            ConnectionProvider = dBConnectionProvider;
            ConnectionProvider.ConnectionString = configuretion.GetConnectionString("DBMSToolConnection").ToString();//配置连接字符串
            ConnectionProvider.DBCategory = DBCategory.SqlServer;
        }


    }
}
