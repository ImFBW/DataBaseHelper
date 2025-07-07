using DBH.DALServiceProvider;
using DBH.Models.Common;
using DBH.Models.EntityViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.DALProvider.MainDAL
{
    /// <summary>
    /// 关于配置的查询-MySql数据库类型
    /// </summary>
    public interface IMySqlManagerDALProvider : IBaseDALProvider
    {
        bool TestConnection(string connectionString);

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        void SetConnectionString(string connectionstring);

        /// <summary>
        /// 进行搜索-搜索数据库对象
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="top">默认查询的数量(合并查询，结果可能超出此值)，
        /// 太多没意义，分页没必要，可以通过精确搜索查出范围内的数据</param>
        /// <returns></returns>
        Task<IList<SysDataBaseSearchView>> SearchActionAsync(string searchText, int top = 100);

        /// <summary>
        /// 查询表的全部列，返回List
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Task<IList<DB_TableColumnsView>> GetTableColumnsListAsync(string tableName);

        /// <summary>
        /// 更新表、字段的说明
        /// </summary>
        /// <param name="tableColumnDescription">数据实体类</param>
        /// <returns></returns>
        Task<EntityResult> UpdateTableColumnDescriptionAsync(TableColumnDescription tableColumnDescription);

        /// <summary>
        /// 查询数据库版本
        /// </summary>
        /// <returns></returns>
        Task<string> GetMySqlVersion();

    }
}
