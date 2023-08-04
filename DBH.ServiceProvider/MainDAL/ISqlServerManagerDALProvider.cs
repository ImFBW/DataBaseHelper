using DBH.DALServiceProvider;
using DBH.Models.EntityViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALProvider.MainDAL
{
    /// <summary>
    /// 关于配置的查询-sqlserver数据库类型
    /// </summary>
    public interface ISqlServerManagerDALProvider : IBaseDALProvider
    {
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        void SetConnectionString(string connectionstring);

        /// <summary>
        /// 进行搜索-SQLServer数据库模式
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="top">默认查询的数量(合并查询，结果可能超出此值)，
        /// 太多没意义，分页没必要，可以通过精确搜索查出范围内的数据</param>
        /// <returns></returns>
        Task<IList<SysDataBaseSearchView>> SearchAction(string searchText, int top = 100);

        /// <summary>
        /// 查询存储过程、表值函数，返回结果集(以行为单位显示)
        /// </summary>
        /// <param name="dbTypeName">存储过程名或函数名</param>
        /// <returns></returns>
        Task<IList<Definition>> GetDefinitionsAsync(string dbTypeName);

    }
}
