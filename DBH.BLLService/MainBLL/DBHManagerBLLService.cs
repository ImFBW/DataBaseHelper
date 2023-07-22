using DBH.BLLServiceProvider.MainBLL;
using DBH.Core;
using DBH.DALServiceProvider.MainDAL;
using DBH.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService.MainBLL
{
    public class DBHManagerBLLService : BaseBLLService, IDBHManagerBLLProvider
    {
        private readonly IDBHManagerDALProvider _dBHManagerDALProvider;

        public DBHManagerBLLService(IDBHManagerDALProvider dBHManagerDALProvider)
        {
            _dBHManagerDALProvider = dBHManagerDALProvider;
        }

        /// <summary>
        /// 获取全部的服务配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<FS_ServicesEntity>> GetServicesConfigListAsync()
        {
            return await _dBHManagerDALProvider.GetServicesConfigListAsync();
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public async Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID)
        {
            if (ID <= 0) return new FS_ServicesEntity();
            return await _dBHManagerDALProvider.GetServicesEnvityAsync(ID);
        }

    }
}
