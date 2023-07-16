using DBH.BLLProvider.MainBLL;
using DBH.DALProvider.MainDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService.MainBLL
{
    public class LogBLLService : BaseBLLService, ILogBLLProvider
    {
        private readonly ILogDALProvider _logDALProvider;
        public LogBLLService(ILogDALProvider logDALProvider)
        {
            _logDALProvider = logDALProvider;
        }
    }
}
