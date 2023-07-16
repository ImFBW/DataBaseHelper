using DBH.BLLProvider.MainBLL;
using DBH.DALProvider.MainDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService.MainBLL
{
    public class MySqlManagerBLLServer : BaseBLLService, IMySqlManagerBLLProvider
    {
        private readonly IMySqlManagerDALProvider _mySqlManagerDALProvider;
        public MySqlManagerBLLServer(IMySqlManagerDALProvider mySqlManagerDALProvider)
        {
            _mySqlManagerDALProvider = mySqlManagerDALProvider;
        }

    }
}
