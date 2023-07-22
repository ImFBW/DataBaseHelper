using DBH.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALServiceProvider.MainDAL
{
    public interface IDBHManagerDALProvider : IBaseDALProvider
    {
        /// <summary>
        /// 获取全部的服务配置
        /// </summary>
        /// <returns></returns>
        Task<IList<FS_ServicesEntity>> GetServicesConfigListAsync();

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID);


    }
}
