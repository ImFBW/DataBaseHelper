﻿using DataBaseHelper.Common;
using DBH.BLLProvider.MainBLL;
using DBH.BLLService.MainBLL;
using DBH.BLLServiceProvider.MainBLL;
using DBH.Core;
using DBH.Core.Setting;
using DBH.DALProvider.MainDAL;
using DBH.Models.Common;
using DBH.Models.Config;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
using DBH.Utils.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;

namespace DataBaseHelper.Controllers
{
    /// <summary>
    /// 主要的控制器文件
    /// </summary>
    public class DataBaseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBHManagerBLLProvider _DBHManagerBLLProvider;
        private readonly ISqlServerManagerBLLProvider _sqlServerManagerBLLProvider;
        private readonly IMySqlManagerBLLProvider _mySqlManagerBLLProvider;
        private readonly IOptionsSnapshot<DBHSetting> _dbhSetting;//建议使用IOptionsSnapshot
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptionsSnapshot<DBConnectionConfig> _dbConnectionConfig;

        public DataBaseController(ILogger<HomeController> logger, IDBHManagerBLLProvider dBHManagerBLLProvider, ISqlServerManagerBLLProvider sqlServerManagerBLLProvider, IOptionsSnapshot<DBHSetting> dbhSetting, IWebHostEnvironment webHostEnvironment, JWTHelper jWTHelper, IOptionsSnapshot<DBConnectionConfig> dbConnectionConfig, IMySqlManagerBLLProvider mySqlManagerBLLProvider)
        {
            _logger = logger;
            _DBHManagerBLLProvider = dBHManagerBLLProvider;
            _sqlServerManagerBLLProvider = sqlServerManagerBLLProvider;
            _dbhSetting = dbhSetting;
            _webHostEnvironment = webHostEnvironment;
            _dbConnectionConfig = dbConnectionConfig;
            _mySqlManagerBLLProvider = mySqlManagerBLLProvider;
        }
        #region DataBase 页面
        /// <summary>
        /// 首页，选择服务器页面（默认页面）
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            IList<FS_ServicesView> listEntity = new List<FS_ServicesView>();
            listEntity = await _DBHManagerBLLProvider.GetServicesConfigListAsync();
            ViewData["listEntity"] = listEntity;
            return View(listEntity);
        }

        /// <summary>
        /// 搜索页，主要的搜索功能的页面，可搜索表、存储过程、表值函数
        /// </summary>
        /// <param name="ID">数据库配置表主键ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Search(int? ID)
        {
            int IDval = 0;
            if (ID.HasValue) IDval = ID.Value;
            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            if (IDval > 0)
            {
                fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(IDval);
            }

            string currentDBAddress = "";//当前选择的数据库地址
            string currentDBName = "--";
            if (fsServiceEntity != null && !string.IsNullOrEmpty(fsServiceEntity.ServerAddress))
            {
                currentDBAddress = fsServiceEntity.ServerAddress;
                if (fsServiceEntity.ServerPortNo > 0)
                    currentDBAddress += ":" + fsServiceEntity.ServerPortNo.ToString();
                currentDBName = fsServiceEntity.DataBaseName;
            }
            ViewBag.CurrentDBName = currentDBName;
            ViewBag.CurrentDBAddress = currentDBAddress;
            return View(fsServiceEntity);
        }

        /// <summary>
        ///  视图-弹出框-添加数据库
        /// </summary>
        /// <param name="id">大于0表示编辑</param>
        /// <returns></returns>
        public async Task<IActionResult> _ViewAddDataBase(int? id)
        {
            int databaseIDval = 0;
            if (id.HasValue) databaseIDval = id.Value; else databaseIDval = 0;
            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            if (databaseIDval > 0)
            {
                fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(databaseIDval);
            }
            IList<FS_ServiceSourceEntity> listSourceEntity = new List<FS_ServiceSourceEntity>();
            listSourceEntity = await _DBHManagerBLLProvider.GetFSServiceSrouceListAsync();
            ViewData["listEntity"] = listSourceEntity;

            Dictionary<int, string> dictDBconfig = new Dictionary<int, string>();
            Array dbArr = Enum.GetValues(typeof(DBCategory));
            foreach (DBCategory dbCategory in dbArr)
            {
                dictDBconfig.Add((int)dbCategory, dbCategory.ToString());
            }
            ViewData["DBCategory"] = dictDBconfig;

            return View(fsServiceEntity);
        }

        /// <summary>
        /// 获取不同的结果的页面的HTML
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeID"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public async Task<IActionResult> PartialViewForSearchResult(int? id, int? typeID, string typeName)
        {
            int databaseIDval = 0;
            if (id.HasValue) databaseIDval = id.Value; else databaseIDval = 0;
            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            if (databaseIDval > 0)
            {
                fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(databaseIDval);
            }
            if (!typeID.HasValue || string.IsNullOrEmpty(typeName))
            {
                return Content("");
            }
            if (fsServiceEntity == null || fsServiceEntity.ID <= 0 || string.IsNullOrEmpty(fsServiceEntity.ServerAddress) || string.IsNullOrEmpty(fsServiceEntity.DataBaseName))
            {
                return Content("");
            }
            string connectionString = string.Empty;
            //获取连接字符串
            if (fsServiceEntity.ServerType == 1)//SqlServer
                connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
            else if (fsServiceEntity.ServerType == 2)//MySQL
                connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);

            if (fsServiceEntity.ServerType == 1)//SqlServer
            {
                _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
            }
            else if (fsServiceEntity.ServerType == 2)//MySQL
            {
                _mySqlManagerBLLProvider.SetConnectionString(connectionString);
            }

            ViewBag.TypeID = typeID;
            ViewBag.TypeName = typeName;
            ViewData["fsServiceEntity"] = fsServiceEntity;
            SysDataBaseSearchView searchView = new SysDataBaseSearchView()
            {
                DBObjectType = (DBObjectType)(typeID.Value),
                TypeName = (string)typeName
            };
            //根据不同的数据类型，返回不同的页面代码
            if (typeID == (int)DBObjectType.U || typeID == (int)DBObjectType.U_C)
            {

                return View("~/Views/Component/_PartialViewSearchAsTable.cshtml", searchView);
            }
            else if (typeID == (int)DBObjectType.P)
            {
                IList<Definition> listDefinition = await _sqlServerManagerBLLProvider.GetDefinitionsAsync(typeName);
                if (listDefinition != null)
                    searchView.Definition = listDefinition.ToList();
                else
                    searchView.Definition = new List<Definition>();
                return View("~/Views/Component/_PartialViewSearchAsProc.cshtml", searchView);
            }
            else if (typeID == (int)DBObjectType.TF)
            {
                IList<Definition> listDefinition = await _sqlServerManagerBLLProvider.GetDefinitionsAsync(typeName);
                if (listDefinition != null)
                    searchView.Definition = listDefinition.ToList();
                else
                    searchView.Definition = new List<Definition>();
                return View("~/Views/Component/_PartialViewSearchAsTFunc.cshtml", searchView);
            }
            else
            {
                return NotFound("类型错误");
            }
        }

        /// <summary>
        /// 生成C# Class
        /// </summary>
        /// <param name="ID">数据库配置ID</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateClass(int? ID, string tableName)
        {
            int databaseIDval = 0;
            if (ID.HasValue) databaseIDval = ID.Value; else databaseIDval = 0;
            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            if (databaseIDval > 0)
            {
                fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(databaseIDval);
            }
            if (fsServiceEntity == null || fsServiceEntity.ID <= 0 || string.IsNullOrEmpty(fsServiceEntity.ServerAddress) || string.IsNullOrEmpty(fsServiceEntity.DataBaseName))
            {
                return NotFound("service missing");
            }
            List<string> listClass = new List<string>();
            string connectionString = string.Empty;
            //配置连接字符串
            if (fsServiceEntity.ServerType == 1)//SqlServer
                connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
            else if (fsServiceEntity.ServerType == 2)//MySQL
                connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
            _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
            if (fsServiceEntity.ServerType == 1)//SqlServer
            {
                listClass = await _sqlServerManagerBLLProvider.CreateNetClass(tableName);
            }
            else if (fsServiceEntity.ServerType == 2)//MySQL
            {
                listClass = await _mySqlManagerBLLProvider.CreateNetClass(tableName);
            }
            ViewData["listClass"] = listClass;
            return View("~/Views/Component/_CreateClass.cshtml");
        }
        #endregion


        #region AjaxRequest的处理
        /// <summary>
        /// 保存数据库配置
        /// </summary>
        /// <param name="fSService"></param>
        /// <returns></returns>
        public async Task<IActionResult> DBServcieSave(RequestFSService fSService)
        {
            var fsEntity = fSService.ToFSServiceEntity;
            fsEntity.IsInUse = 1;
            EntityResult entityResult = new EntityResult();

            if (fsEntity.ID > 0)//更新
            {
                entityResult = await _DBHManagerBLLProvider.UpdateFsServiceEntityAsync(fsEntity);
            }
            else//新增
            {
                entityResult = await _DBHManagerBLLProvider.InsertFsServiceEntityAsync(fsEntity);
            }

            _logger.LogInformation("Save fs Service:DataBaseName:" + fSService.DBName, DateTime.UtcNow);

            return Json(entityResult);
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestConnection()
        {
            ResultModel result = new ResultModel();
            string dbAddress = Request.Query["DBAddress"].ToString();
            string dbPort = Request.Query["DBPort"].ToString();
            string dbName = Request.Query["DBName"].ToString();
            string dbLoginName = Request.Query["DBLoginName"].ToString();
            string dbLoginPassword = Request.Query["DBLoginPassword"].ToString();
            string dbCategory = Request.Query["DBCategory"].ToString();

            DBCategory _dbCategory;
            int portNum = 0;
            string connectionString = string.Empty;

            if (!Enum.TryParse(dbCategory, out _dbCategory))
            {
                _dbCategory = DBCategory.SqlServer;//默认
            }
            int.TryParse(dbPort, out portNum);

            //配置连接字符串
            if (_dbCategory == DBCategory.SqlServer)
            {
                connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(dbAddress, portNum, dbName, dbLoginName, dbLoginPassword);
                _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
            }
            else if (_dbCategory == DBCategory.MySql)
            {
                connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(dbAddress, portNum, dbName, dbLoginName, dbLoginPassword);
                _mySqlManagerBLLProvider.SetConnectionString(connectionString);
            }

            try
            {
                bool isConn = false;
                if (_dbCategory == DBCategory.SqlServer)
                    isConn = _sqlServerManagerBLLProvider.TestConnection(connectionString);
                if (_dbCategory == DBCategory.MySql)
                    isConn = _mySqlManagerBLLProvider.TestConnection(connectionString);
                result.Result = isConn ? "true" : "false";
                result.Message = isConn ? "连接成功" : "连接失败";
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Result = "false";
                result.Status = false;
                result.Message = "异常：" + ex.Message + "\r connectionString=[" + connectionString + "]";
            }
            return Json(result);
        }

        /// <summary>
        /// 执行删除数据的操作
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteDatabase(int? ID)
        {
            int IDval = 0;
            if (ID.HasValue) IDval = ID.Value;
            ResultModel result = new ResultModel();

            if (IDval <= 0)
            {
                result.Code = ResultCode.Error;
                result.Status = false;
                result.Message = "ID 为空";
            }
            else
            {
                bool deleteCode = await _DBHManagerBLLProvider.DeleteFsServiceEntityAsync(IDval);
                if (deleteCode)
                {
                    result.Code = ResultCode.Success;
                    result.Status = true;
                    result.Message = "删除成功";
                }
                else
                {
                    result.Code = ResultCode.Fail;
                    result.Status = false;
                    result.Message = "删除失败";
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索：表名、表字段名、存储过程名、表值函数名
        /// </summary>
        /// <param name="SearchTxt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SearchSubmit(int? ID, string SearchTxt)
        {
            ResultModel result = new ResultModel();
            result.Status = false;
            int IDval = 0;
            if (ID.HasValue) { IDval = ID.Value; }
            else
            {
                result.Message = "未选择数据库";
                return Json(result);
            }
            if (string.IsNullOrEmpty(SearchTxt))
            {
                result.Message = "搜索参数为空";
                return Json(result);
            }

            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(IDval);
            if (fsServiceEntity == null || fsServiceEntity.ID <= 0 || string.IsNullOrEmpty(fsServiceEntity.ServerAddress) || string.IsNullOrEmpty(fsServiceEntity.DataBaseName))
            {
                result.Message = "数据库配置错误";
                return Json(result);
            }
            result.Message = SearchTxt;
            IList<SysDataBaseSearchView> listView = new List<SysDataBaseSearchView>();
            try
            {
                string connectionString =string.Empty;
                //配置数据库类型
                if (fsServiceEntity.ServerType == 1)//SqlServer
                {
                    connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                    _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
                }
                else if (fsServiceEntity.ServerType == 2)//MySQL
                {
                    connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                    _mySqlManagerBLLProvider.SetConnectionString(connectionString);
                }


                if (fsServiceEntity.ServerType == 1)//SqlServer
                {
                    listView = await _sqlServerManagerBLLProvider.SearchActionAsync(SearchTxt);
                }
                else if (fsServiceEntity.ServerType == 2)//MySQL
                {
                    listView = await _mySqlManagerBLLProvider.SearchActionAsync(SearchTxt);
                }
                result.Status = true;
                result.Result = listView;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 加载Table的字段数据，一次性加载全部字段，前端分页
        /// </summary>
        /// <param name="ID">服务数据库配置ID</param>
        /// <param name="tableName">要搜索的表名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetTableData(int? ID, string tableName)
        {
            ResultModel result = new ResultModel();
            result.Status = false;
            int IDval = 0;
            if (ID.HasValue) { IDval = ID.Value; }
            else
            {
                result.Message = "未选择数据库";
                return Json(result);
            }
            if (string.IsNullOrEmpty(tableName))
            {
                result.Message = "表名参数为空";
                return Json(result);
            }
            IList<DB_TableColumnsView> listColumn = await _DBHManagerBLLProvider.GetServiceTableColumns(IDval, tableName);
            result.Result = listColumn;
            result.Status = true;
            result.Message = "success";
            //string resultJson = JsonConvert.SerializeObject(listColumn, Formatting.Indented);
            //Json格式的要求{total:123,rows:{}}
            //构造成Json的格式传递
            var resultJson = new { total = listColumn.Count, rows = listColumn.ToList() };

            string stringJson = JsonConvert.SerializeObject(resultJson, Formatting.Indented);
            return Content(stringJson);
        }

        /// <summary>
        /// 更新表、字段的说明
        /// </summary>
        /// <param name="ID">当前选择的数据库配置ID</param>
        /// <param name="tableColumnDescription">数据实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTableColumnDesc(int? ID, TableColumnDescription tableColumnDescription)
        {
            ResultModel result = new ResultModel();
            result.Status = false;
            int IDval = 0;
            if (ID.HasValue) { IDval = ID.Value; }
            else
            {
                result.Message = "未选择数据库";
                return Json(result);
            }
            if (string.IsNullOrEmpty(tableColumnDescription.TableName))
            {
                result.Message = "表名参数为空";
                return Json(result);
            }
            if (tableColumnDescription.TypeID == 2 && string.IsNullOrEmpty(tableColumnDescription.TableColumn))
            {
                result.Message = "列名参数为空";
                return Json(result);
            }
            FS_ServicesEntity fsServiceEntity = new FS_ServicesEntity();
            fsServiceEntity = await _DBHManagerBLLProvider.GetServicesEnvityAsync(IDval);
            if (fsServiceEntity == null || fsServiceEntity.ID <= 0 || string.IsNullOrEmpty(fsServiceEntity.ServerAddress) || string.IsNullOrEmpty(fsServiceEntity.DataBaseName))
            {
                result.Message = "数据库配置错误";
                return Json(result);
            }
            EntityResult entityResult = new EntityResult();
            string connectionString = string.Empty;
            //配置数据库类型
            if (fsServiceEntity.ServerType == 1)//SqlServer
            {
                connectionString = _dbConnectionConfig.Value.GetMsSqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                _sqlServerManagerBLLProvider.SetConnectionString(connectionString);
            }
            else if (fsServiceEntity.ServerType == 2)//MySQL
            {
                connectionString = _dbConnectionConfig.Value.GetMySqlConnectionString(fsServiceEntity.ServerAddress, fsServiceEntity.ServerPortNo, fsServiceEntity.DataBaseName, fsServiceEntity.LoginName, fsServiceEntity.LoginPassword);
                _mySqlManagerBLLProvider.SetConnectionString(connectionString);
            }

            if (fsServiceEntity.ServerType == 1)//SqlServer
            {
                entityResult = await _sqlServerManagerBLLProvider.UpdateTableColumnDescriptionAsync(tableColumnDescription);
            }
            else if (fsServiceEntity.ServerType == 2)//MySQL
            {
                entityResult = await _mySqlManagerBLLProvider.UpdateTableColumnDescriptionAsync(tableColumnDescription);
            }
            if (entityResult.EntityCode == EntityCode.Success)
            {
                result.Status = true;
                result.Message = "success";
            }
            else
            {
                result.Status = false;
                result.Message = entityResult.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExportToExcel(int? ID, string tableName)
        {
            ResultModel result = new ResultModel();
            result.Status = false;
            int IDval = 0;
            if (ID.HasValue) { IDval = ID.Value; }
            else
            {
                result.Message = "未选择数据库";
                return Json(result);
            }
            if (string.IsNullOrEmpty(tableName))
            {
                result.Message = "表名参数为空";
                return Json(result);
            }
            IList<DB_TableColumnsView> listColumn = await _DBHManagerBLLProvider.GetServiceTableColumns(IDval, tableName);
            string filePath = _dbhSetting.Value.ExportFilePath;
            string exportFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            string fileName = listColumn[0].TableName + DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
            if (listColumn != null && listColumn.Count > 0)
            {
                try
                {
                    NPOIHelper.ExportToExcel(listColumn.ToList(), exportFilePath, fileName);
                    result.Message = "success";
                    string fileUrl = _dbhSetting.Value.ExportFileHttpURL + fileName;
                    //string fileUrl=Request.
                    result.Result = fileUrl;
                    result.Status = true;
                }
                catch (IOException)
                {
                    result.Message = "文件被占用，请关闭或稍后再试";
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
            //return File(fileStream, "application/vnd.ms-excel", fileName);
        }
        #endregion
    }
}
