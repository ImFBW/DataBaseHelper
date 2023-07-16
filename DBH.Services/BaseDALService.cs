using DBH.DALProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALServices
{
    public abstract class BaseDALService
    {
        public IDBConnectionProvider ConnectionProvider { get; internal set; }

    }
}
