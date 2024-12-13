using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DBH.Core.Setting
{
    /// <summary>
    /// 关于数据库连接的一些配置
    /// </summary>
    public class DBConnectionConfig
    {
        private string _MSSqlConnectionStringTemplate;
        private string _MySqlConnectionStringTemplate;

        public DBConnectionConfig()
        {
            //来自配置setting，理论上不能为空，这里可以填入默认模板字符串
            _MSSqlConnectionStringTemplate = "";
            _MySqlConnectionStringTemplate = "";
        }

        /// <summary>
        /// Sqlserver连接字符串模板
        /// </summary>
        public string MSSqlConnectionStringTemplate
        {
            get
            {
                return _MSSqlConnectionStringTemplate;
            }
            set
            {
                _MSSqlConnectionStringTemplate = value;
            }
        }
        /// <summary>
        /// MySql连接字符串模板
        /// </summary>
        public string MySqlConnectionStringTemplate
        {
            get
            {
                return _MySqlConnectionStringTemplate;
            }
            set
            {
                _MySqlConnectionStringTemplate = value;
            }
        }

        /// <summary>
        /// 返回Sql Server连接字符串
        /// </summary>
        /// <param name="server">服务器地址</param>
        /// <param name="port">端口号</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="userid">登录ID</param>
        /// <param name="password">登录密码</param>
        public string GetMsSqlConnectionString(string server, int port, string dbName, string userid, string password)
        {
            string portStr = string.Empty;
            if (port > 0)
            {
                portStr = ":" + port.ToString();
            }
            return this.MSSqlConnectionStringTemplate.Replace("{Server}", server)
                  .Replace("{DBName}", dbName)
                  .Replace("{userid}", userid)
                  .Replace("{Port}", portStr)
                  .Replace("{password}", password);
        }
        /// <summary>
        /// 返回MySql连接字符串
        /// </summary>
        /// <param name="server">服务器地址</param>
        /// <param name="port">端口号</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="userid">登录ID</param>
        /// <param name="password">登录密码</param>
        public string GetMySqlConnectionString(string server, int port, string dbName, string userid, string password)
        {
            string portStr = string.Empty;
            if (port > 0) { portStr = port.ToString(); }
            return this.MySqlConnectionStringTemplate.Replace("{Server}", server)
                  .Replace("{DBName}", dbName)
                  .Replace("{userid}", userid)
                  .Replace("{Port}", portStr)
                  .Replace("{password}", password);
        }
    }
}
