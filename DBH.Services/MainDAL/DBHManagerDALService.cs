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
using DBH.Models.Common;
using DBH.Core.Dapper;

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
        #region Insert

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="fS_ServicesEntity"></param>
        /// <returns></returns>
        public async Task<EntityResult> InsertFsServiceEntity(FS_ServicesEntity fS_ServicesEntity)
        {
            EntityResult entiyResult = new EntityResult();
            using (var conn = ConnectionProvider.GetConnection())
            {
                //string sql = $"SELECT s.*,ss.SourceName From FS_Services  s WITH(NOLOCK) LEFT JOIN FS_ServiceSource ss WITH(NOLOCK) ON s.SourceID=ss.ID WHERE s.IsInUse=1 AND s.IsDel=0  ORDER BY s.id ASC";
                //var resultData = await conn.q
                int? newID = await conn.InsertAsync(fS_ServicesEntity);
                if (newID.HasValue)
                {
                    entiyResult.ID = newID.Value;
                    entiyResult.EntityCode = EntityCode.Success;
                }
                else
                {
                    entiyResult.ID = -1;
                    entiyResult.EntityCode = EntityCode.Fail;
                }
            }
            return entiyResult;
        }
        #endregion

        #region Select
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

        /// <summary>
        /// 测试一个连接字符串是否可以打开连接成功
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public async Task<bool> TestConnection(string connectionString)
        {
            bool isConn = false;
            try
            {
                using (var conn = ConnectionProvider.GetConnection(connectionString))
                {
                    conn.Open();
                    isConn = true;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                isConn = false;
            }
            return isConn;
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新数据库配置，密码可空，则不更新密码
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<EntityResult> UpdateFsServiceEntity(FS_ServicesEntity entity)
        {
            EntityResult result = new EntityResult();
            using (var conn = ConnectionProvider.GetConnection())
            {
                string[] ignoreFields = { ""};
                if (string.IsNullOrEmpty(entity.LoginPassword))//如果密码为空，则默认为不更新，可忽略此字段的更新
                    ignoreFields[0] = "LoginPassword";
                int code = await conn.UpdateIgnoreAppointAsync<FS_ServicesEntity>(entity, ignoreFields);
                if (code > 0)
                {
                    result.ID = entity.ID;
                    result.EntityCode = EntityCode.Success;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除一条数据库配置数据
        /// [假删除，更新Isdel=1]
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteFsServiceEntity(int ID)
        {
            bool isDelete = false;
            using (var conn = ConnectionProvider.GetConnection())
            {
                FS_ServicesEntity fsentity = new FS_ServicesEntity() { ID = ID, IsDel = 1 };
                int code = await conn.UpdateAppointAsync<FS_ServicesEntity>(fsentity, new string[] { "IsDel" });
                isDelete = code > 0;
            }
            return isDelete;
        }
        #endregion

        #region Delete

        #endregion


    }
}
