using DBH.BLLProvider.MainBLL;
using DBH.Core;
using DBH.DALProvider.MainDAL;
using DBH.DALServiceProvider.MainDAL;
using DBH.Models.Common;
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
        /// 测试一个连接字符串是否可以打开连接成功
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public bool TestConnection(string connectionString)
        {
            return _sqlServerManagerDALProvider.TestConnection(connectionString);
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
        public async Task<IList<SysDataBaseSearchView>> SearchActionAsync(string searchText, int top = 100)
        {
            try
            {
                return await _sqlServerManagerDALProvider.SearchActionAsync(searchText, top);
            }
            catch (Exception ex)
            {
                Logger.LogError("SQLServer_SearchActionAsync：" + ex.Message);
                throw ex;
            }
        }


        /// <summary>
        /// 查询存储过程、表值函数，返回结果集(以行为单位显示)
        /// </summary>
        /// <param name="dbTypeName">存储过程名或函数名</param>
        /// <returns></returns>
        public async Task<IList<Definition>> GetDefinitionsAsync(string dbTypeName)
        {
            try
            {
                if (string.IsNullOrEmpty(dbTypeName)) return null;
                return await _sqlServerManagerDALProvider.GetDefinitionsAsync(dbTypeName);
            }
            catch (Exception ex)
            {
                Logger.LogError("SQLServer_GetDefinitionsAsync：" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 查询表的全部列，返回List
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IList<DB_TableColumnsView>> GetTableColumnsListAsync(string tableName)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName)) return null;
                return await _sqlServerManagerDALProvider.GetTableColumnsListAsync(tableName);
            }
            catch (Exception ex)
            {
                Logger.LogError("SQLServer_GetTableColumnsListAsync：" + ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// 更新表、字段的说明
        /// </summary>
        /// <param name="tableColumnDescription">数据实体类</param>
        /// <returns></returns>
        public async Task<EntityResult> UpdateTableColumnDescriptionAsync(TableColumnDescription tableColumnDescription)
        {
            try
            {
                EntityResult result = new EntityResult();
                if (string.IsNullOrEmpty(tableColumnDescription.TableName))
                {
                    result.EntityCode = EntityCode.PramIsNull;
                    result.Message = "表名为空";
                }
                if (tableColumnDescription.TypeID == 2)
                {
                    if (string.IsNullOrEmpty(tableColumnDescription.TableColumn))
                    {
                        result.EntityCode = EntityCode.PramIsNull;
                        result.Message = "列名为空";
                    }
                }
                if (!string.IsNullOrEmpty(result.Message)) return result;
                return await _sqlServerManagerDALProvider.UpdateTableColumnDescriptionAsync(tableColumnDescription);
            }
            catch (Exception ex)
            {
                Logger.LogError("SQLServer_UpdateTableColumnDescriptionAsync：" + ex.Message);
                throw ex;
            }


        }

        /// <summary>
        /// 对某个表生成Class，Net版
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<List<string>> CreateNetClass(string tableName)
        {
            List<string> listResult = new List<string>();
            List<ColumnTypeDescView> listColumn = new List<ColumnTypeDescView>();
            IList<DB_TableColumnsView> listData = await GetTableColumnsListAsync(tableName);
            foreach (var item in listData)
            {
                ColumnTypeDescView col = new ColumnTypeDescView()
                {
                    ColumnName = item.ColumnName,
                    ColumnDataType = item.ColumnDataType,
                    ColumnDesc = item.ColumnDesc,
                };
                listColumn.Add(col);
            }
            if (listColumn.Count>0)
            {
                listResult = DataHelper.CreateNetClass(listColumn).ToList();
            }        
            return listResult;
        }
    }
}
