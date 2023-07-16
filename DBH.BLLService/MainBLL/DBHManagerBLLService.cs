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
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<FS_ServicesEntity> TestAsync(int ID)
        {
            return await _dBHManagerDALProvider.TestAsync(ID);
        }
    }
}
