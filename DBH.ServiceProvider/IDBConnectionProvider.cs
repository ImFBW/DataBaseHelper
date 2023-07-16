using DBH.Core;
using System.Data;

namespace DBH.DALProvider
{
    /// <summary>
    /// 提供不同数据库的连接方式
    /// </summary>
    public interface IDBConnectionProvider
    {
        /// <summary>
        /// 数据库类别，SqlServer、MsSql等
        /// </summary>
        DBCategory DBCategory { get; set; }

        /// <summary>
        /// 连接字符串，对应不同的数据库类别
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 获取数据连接方式
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
