using DBH.BLLProvider.MainBLL;
using DBH.BLLServiceProvider.MainBLL;
using DBH.Core;
using DBH.Core.Setting;
using DBH.DALServiceProvider.MainDAL;
using DBH.Models.Common;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService.MainBLL
{
    public class DBHManagerBLLService : BaseBLLService, IDBHManagerBLLProvider
    {
        private readonly IDBHManagerDALProvider _dBHManagerDALProvider;
        private readonly ISqlServerManagerBLLProvider _sqlServerManagerBLLProvider;
        private readonly IMySqlManagerBLLProvider _mySqlManagerBLLProvider;
        private readonly IOptions<DBConnectionConfig> _dbConnectionConfig;

        public DBHManagerBLLService(IDBHManagerDALProvider dBHManagerDALProvider, ISqlServerManagerBLLProvider sqlServerManagerBLLProvider, IOptions<DBConnectionConfig> dbConnectionConfig, IMySqlManagerBLLProvider mySqlManagerBLLProvider)
        {
            _dBHManagerDALProvider = dBHManagerDALProvider;
            _sqlServerManagerBLLProvider = sqlServerManagerBLLProvider;
            _dbConnectionConfig = dbConnectionConfig;
            _mySqlManagerBLLProvider = mySqlManagerBLLProvider;
        }

        #region Select
        /// <summary>
        /// 获取全部的服务配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<FS_ServicesView>> GetServicesConfigListAsync()
        {
            try
            {
                IList<FS_ServicesView> listEntity = await _dBHManagerDALProvider.GetServicesConfigListAsync();
                if (listEntity == null) return new List<FS_ServicesView>();
                return listEntity;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_GetServicesConfigListAsync：" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public async Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID)
        {
            try
            {
                if (ID <= 0) return new FS_ServicesEntity();
                FS_ServicesEntity entity = await _dBHManagerDALProvider.GetServicesEnvityAsync(ID);
                if (entity == null) return new FS_ServicesEntity();
                return entity;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_GetServicesEnvityAsync：" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 查询全部的列
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public async Task<IList<DB_TableColumnsView>> GetServiceTableColumns(int ID, string tableName)
        {
            try
            {
                if (ID <= 0) return null;
                FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
                fsServiceEntity = await GetServicesEnvityAsync(ID);

                if (fsServiceEntity == null || fsServiceEntity.ID <= 0 || string.IsNullOrEmpty(fsServiceEntity.ServerAddress) || string.IsNullOrEmpty(fsServiceEntity.DataBaseName))
                {
                    throw new ArgumentException("数据库配置错误");
                }
                IList<DB_TableColumnsView> listColumn = new List<DB_TableColumnsView>();
                string connectionString = string.Empty;

                //配置数据库类型
                if (fsServiceEntity.ServerType == 1)//SqlServer
                {
                    connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                    _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
                }
                else if (fsServiceEntity.ServerType == 2)//MySQL
                {
                    connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                    _mySqlManagerBLLProvider.SetConnectionString(connectionString);
                }
                
                if (fsServiceEntity.ServerType == 1)//SqlServer
                {
                    listColumn = await _sqlServerManagerBLLProvider.GetTableColumnsListAsync(tableName);
                }
                else if (fsServiceEntity.ServerType == 2)//MySQL
                {
                    listColumn = await _mySqlManagerBLLProvider.GetTableColumnsListAsync(tableName);
                }
                return listColumn;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_GetServicesEnvityAsync：" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获取全部的服务器来源配置
        /// </summary>
        /// <returns></returns>
        public async Task<IList<FS_ServiceSourceEntity>> GetFSServiceSrouceListAsync()
        {
            try
            {
                //to do ...考虑增加缓存
                IList<FS_ServiceSourceEntity> listEntity = await _dBHManagerDALProvider.GetFSServiceSrouceListAsync();
                if (listEntity == null) return new List<FS_ServiceSourceEntity>();
                return listEntity;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_GetFSServiceSrouceListAsync：" + ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Insert
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="fS_ServicesEntity"></param>
        /// <returns></returns>
        public async Task<EntityResult> InsertFsServiceEntityAsync(FS_ServicesEntity fS_ServicesEntity)
        {
            try
            {
                #region 参数判断
                EntityResult result = new EntityResult();
                if (string.IsNullOrEmpty(fS_ServicesEntity.ServerAddress))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "数据库地址不可为空";
                }
                else if (fS_ServicesEntity.SourceID <= 0)
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "来源不可为空";
                }
                else if (string.IsNullOrEmpty(fS_ServicesEntity.DataBaseName))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "数据库名称不可为空";
                }
                else if (string.IsNullOrEmpty(fS_ServicesEntity.LoginName))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "登录名不可为空";
                }
                else if (string.IsNullOrEmpty(fS_ServicesEntity.LoginPassword))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "登陆密码不可为空";
                }
                #endregion

                if (result.Message == "")
                {
                    result = await _dBHManagerDALProvider.InsertFsServiceEntityAsync(fS_ServicesEntity);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_InsertFsServiceEntityAsync：" + ex.Message);
                return new EntityResult() { ID = -1, Message = ex.Message, EntityCode = EntityCode.SysError };
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// 更新数据库配置，密码可空，则不更新密码
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<EntityResult> UpdateFsServiceEntityAsync(FS_ServicesEntity entity)
        {
            try
            {
                #region 参数判断
                EntityResult result = new EntityResult();
                if (string.IsNullOrEmpty(entity.ServerAddress))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "数据库地址不可为空";
                }
                else if (entity.SourceID <= 0)
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "来源不可为空";
                }
                else if (string.IsNullOrEmpty(entity.DataBaseName))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "数据库名称不可为空";
                }
                else if (string.IsNullOrEmpty(entity.LoginName))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "登录名不可为空";
                }
                #endregion
                if (result.Message == "")
                {
                    return await _dBHManagerDALProvider.UpdateFsServiceEntityAsync(entity);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_UpdateFsServiceEntityAsync：" + ex.Message);
                return new EntityResult() { ID = -1, Message = ex.Message, EntityCode = EntityCode.SysError };
            }
        }

        /// <summary>
        /// 删除一条数据库配置数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteFsServiceEntityAsync(int ID)
        {
            try
            {
                return await _dBHManagerDALProvider.DeleteFsServiceEntityAsync(ID);
            }
            catch (Exception ex)
            {
                Logger.LogError("DBHManagerBLLService_DeleteFsServiceEntityAsync：" + ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}
