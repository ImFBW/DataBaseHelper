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
using DBH.Models.EntityViews;

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
        /// 获取全部的服务配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<FS_ServicesView>> GetServicesConfigListAsync()
        {
            //IList<FS_ServicesEntity> listEntity = new List<FS_ServicesEntity>();
            IList<FS_ServicesView> listViewEntity = new List<FS_ServicesView>();
            using (var conn = ConnectionProvider.GetConnection())
            {
                string sql = $"SELECT s.*,ss.SourceName From FS_Services  s WITH(NOLOCK) LEFT JOIN FS_ServiceSource ss WITH(NOLOCK) ON s.SourceID=ss.ID WHERE s.IsInUse=1 AND s.IsDel=0  ORDER BY s.id ASC";
                var resultData = await conn.QueryAsync<FS_ServicesView>(sql, commandType: CommandType.Text);
                listViewEntity = resultData.ToList();
            }
            return listViewEntity;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID)
        {
            FS_ServicesEntity entity = new FS_ServicesEntity();
            using (var conn = ConnectionProvider.GetConnection())
            {
                string sql = $"SELECT Top 1 * From FS_Services WITH(NOLOCK) WHERE ID ={ID}";
                entity = await conn.QueryFirstOrDefaultAsync<FS_ServicesEntity>(sql, commandType: CommandType.Text);
            }
            return entity;
        }

        /// <summary>
        /// 获取全部的服务器来源配置
        /// </summary>
        /// <returns></returns>
        public async Task<IList<FS_ServiceSourceEntity>> GetFSServiceSrouceListAsync()
        {
            IList<FS_ServiceSourceEntity> listViewEntity = new List<FS_ServiceSourceEntity>();
            using (var conn = ConnectionProvider.GetConnection())
            {
                string sql = $"SELECT * From FS_ServiceSource WITH(NOLOCK)";
                var resultData = await conn.QueryAsync<FS_ServiceSourceEntity>(sql, commandType: CommandType.Text);
                listViewEntity = resultData.ToList();
            }
            return listViewEntity;
        }
    }
}
