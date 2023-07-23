using DBH.BLLServiceProvider.MainBLL;
using DBH.Core;
using DBH.DALServiceProvider.MainDAL;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
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
        public async Task<IList<FS_ServicesView>> GetServicesConfigListAsync()
        {
            IList<FS_ServicesView> listEntity = await _dBHManagerDALProvider.GetServicesConfigListAsync();
            if (listEntity == null) return new List<FS_ServicesView>();
            return listEntity;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public async Task<FS_ServicesEntity> GetServicesEnvityAsync(int ID)
        {
            if (ID <= 0) return new FS_ServicesEntity();
            FS_ServicesEntity entity = await _dBHManagerDALProvider.GetServicesEnvityAsync(ID);
            if (entity == null) return new FS_ServicesEntity();
            return entity;
        }

        /// <summary>
        /// 获取全部的服务器来源配置
        /// </summary>
        /// <returns></returns>
        public async Task<IList<FS_ServiceSourceEntity>> GetFSServiceSrouceListAsync()
        {
            //to do ...考虑增加缓存
            IList<FS_ServiceSourceEntity> listEntity = await _dBHManagerDALProvider.GetFSServiceSrouceListAsync();
            if (listEntity == null) return new List<FS_ServiceSourceEntity>();
            return listEntity;
        }



    }
}
