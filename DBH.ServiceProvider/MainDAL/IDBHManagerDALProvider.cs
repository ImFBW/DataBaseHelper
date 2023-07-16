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
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<FS_ServicesEntity> TestAsync(int ID);

    }
}
