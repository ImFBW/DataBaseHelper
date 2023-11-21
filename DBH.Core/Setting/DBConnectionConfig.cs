using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.Core.Setting
{
    /// <summary>
    /// 关于数据库连接的一些配置
    /// </summary>
    public static class DBConnectionConfig
    {
        public static string MSSqlConnectionStringTemplate = "data source={Server};initial catalog={DBName};user id={LoginName};password={Password};";
    }
}
