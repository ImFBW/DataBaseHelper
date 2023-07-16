using DBH.BLLServiceProvider.MainBLL;
using DBH.Core;
using DBH.DALServiceProvider.MainDAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DBH.Models.Entitys;
using System.Data;
using DBH.DALProvider;

namespace DBH.DALServices.MainDAL
{
    public class DBHManagerDALService : BaseDALService, IDBHManagerDALProvider
    {

        public DBHManagerDALService(IConfiguration configuretion, IDBConnectionProvider dBConnectionProvider)
        {
            ConnectionProvider = dBConnectionProvider;
            ConnectionProvider.ConnectionString = configuretion.GetConnectionString("DBMSToolConnection").ToString();//配置连接字符串
            ConnectionProvider.DBCategory = DBCategory.SqlServer;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<FS_ServicesEntity> TestAsync(int ID)
        {
            FS_ServicesEntity entity = new FS_ServicesEntity();
            using (var conn = ConnectionProvider.GetConnection())
            {
                string sql = $"SELECT Top 1 * From FS_Services WHERE ID ={ID}";
                entity = await conn.QueryFirstOrDefaultAsync<FS_ServicesEntity>(sql, commandType: CommandType.Text);
            }
            return entity;
        }
    }
}
