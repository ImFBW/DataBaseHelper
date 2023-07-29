using DBH.Models.Common;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLServiceProvider.MainBLL
{
    public interface IDBHManagerBLLProvider : IBaseBLLProvider
    {
        #region Select
        /// <summary>
        /// 获取全部的服务配置
        /// </summary>
        /// <returns></returns>
        Task<IList<FS_ServicesView>> GetServicesConfigListAsync();

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID);

        /// <summary>
        /// 获取全部的服务器来源配置
        /// </summary>
        /// <returns></returns>
        Task<IList<FS_ServiceSourceEntity>> GetFSServiceSrouceListAsync();

        /// <summary>
        /// 测试一个连接字符串是否可以打开连接成功
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        Task<bool> TestConnection(string connectionString);

        #endregion

        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fS_ServicesEntity"></param>
        /// <returns></returns>
        Task<EntityResult> InsertFsServiceEntity(FS_ServicesEntity fS_ServicesEntity);

        #endregion


    }
}
