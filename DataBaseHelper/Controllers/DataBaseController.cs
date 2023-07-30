using DBH.BLLServiceProvider.MainBLL;
using DBH.Models.Common;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Dapper.SqlMapper;

namespace DataBaseHelper.Controllers
{
    /// <summary>
    /// 主要的控制器文件
    /// </summary>
    public class DataBaseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBHManagerBLLProvider _DBHManagerBLLProvider;
        public DataBaseController(ILogger<HomeController> logger, IDBHManagerBLLProvider dBHManagerBLLProvider)
        {
            _logger = logger;
            _DBHManagerBLLProvider = dBHManagerBLLProvider;
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

            return View(fsServiceEntity);
        }
        #endregion

        #region Search 页面
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

            string currentDBAddress = "--";//当前选择的数据库地址
            if (fsServiceEntity != null)
            {
                currentDBAddress = fsServiceEntity.ServerAddress;
                if (fsServiceEntity.ServerPortNo > 0)
                    currentDBAddress += ":" + fsServiceEntity.ServerPortNo.ToString();
            }
            ViewBag.CurrentDBName = fsServiceEntity.DataBaseName;
            ViewBag.CurrentDBAddress = currentDBAddress;
            return View(fsServiceEntity);
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
            fsEntity.ServerType = (int)ServiceType.MSSql;//当前固定1：SqlServer，后期还要增加MySql的支持
            EntityResult entityResult = new EntityResult();
            if (fsEntity.ID > 0)//更新
            {
                entityResult = await _DBHManagerBLLProvider.UpdateFsServiceEntity(fsEntity);
            }
            else//新增
            {
                entityResult = await _DBHManagerBLLProvider.InsertFsServiceEntity(fsEntity);
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
            string dbAddress = Request.Query["DBAddress"].ToString();
            string dbPort = Request.Query["DBPort"].ToString();
            string dbName = Request.Query["DBName"].ToString();
            string dbLoginName = Request.Query["DBLoginName"].ToString();
            string dbLoginPassword = Request.Query["DBLoginPassword"].ToString();
            if (!string.IsNullOrEmpty(dbPort) && int.Parse(dbPort) > 0)
                dbAddress += ":" + dbPort;
            string connectionString = string.Format($"data source={dbAddress};persist security info=True;initial catalog={dbName};user id={dbLoginName};password={dbLoginPassword};");
            bool isConn = await _DBHManagerBLLProvider.TestConnection(connectionString);
            return Content(isConn ? "true" : "false");
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
                bool deleteCode = await _DBHManagerBLLProvider.DeleteFsServiceEntity(IDval);
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
        public async Task<IActionResult> Search(string SearchTxt)
        {
            ResultModel result = new ResultModel();
            result.Message = SearchTxt;

            return Json(result);
        }
        #endregion
    }
}
