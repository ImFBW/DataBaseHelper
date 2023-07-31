using DBH.BLLProvider.MainBLL;
using DBH.DALProvider.MainDAL;
using DBH.DALServiceProvider.MainDAL;
using DBH.Models.EntityViews;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBH.BLLService.MainBLL
{
    /// <summary>
    /// 关于配置的查询-sqlserver数据库类型
    /// </summary>
    public class SqlServerManagerBLLService : BaseBLLService, ISqlServerManagerBLLProvider
    {
        private readonly ISqlServerManagerDALProvider _sqlServerManagerDALProvider;

        public SqlServerManagerBLLService(ISqlServerManagerDALProvider sqlServerManagerDALProvider)
        {
            _sqlServerManagerDALProvider = sqlServerManagerDALProvider;
        }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SetConnectionString(string connectionstring)
        {
            _sqlServerManagerDALProvider.SetConnectionString(connectionstring);
        }


        /// <summary>
        /// 进行搜索-SQLServer数据库模式
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="top">默认查询的数量(合并查询，结果可能超出此值)，
        /// 太多没意义，分页没必要，可以通过精确搜索查出范围内的数据</param>
        /// <returns></returns>
        public async Task<IList<SysDataBaseSearchView>> SearchAction(string searchText,int top=100)
        {
            try
            {
                return await _sqlServerManagerDALProvider.SearchAction(searchText, top);
            }
            catch (Exception ex)
            {
                Logger.LogError("SQLServer_SearchAction：" + ex.Message);
                throw ex;
            }
        }

    }
}
