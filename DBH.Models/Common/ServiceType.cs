using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Models.Common
{
    /// <summary>
    /// 定义数据库类型，当前仅有支持：MSSqlServer，和MySQL
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// MSSqlServer，对应数据库类型1=MSSqlServer
        /// </summary>
        MSSql = 1,

        /// <summary>
        /// MySQL，对应数据库类型2=MySQL
        /// </summary>
        MySQL = 2,
    }
}
