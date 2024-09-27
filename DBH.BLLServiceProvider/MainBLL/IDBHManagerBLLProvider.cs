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
        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fS_ServicesEntity"></param>
        /// <returns></returns>
        Task<EntityResult> InsertFsServiceEntityAsync(FS_ServicesEntity fS_ServicesEntity);

        #endregion

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

        Task<IList<DB_TableColumnsView>> GetServiceTableColumns(int ID, string tableName);

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
        Task<bool> TestConnectionAsync(string connectionString);

        #endregion

        #region Update

        /// <summary>
        /// 更新数据库配置，密码可空，则不更新密码
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<EntityResult> UpdateFsServiceEntityAsync(FS_ServicesEntity entity);

        /// <summary>
        /// 删除一条数据库配置数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        Task<bool> DeleteFsServiceEntityAsync(int ID);
        #endregion

    }
}
