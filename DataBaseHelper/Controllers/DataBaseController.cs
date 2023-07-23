using DBH.BLLServiceProvider.MainBLL;
using DBH.Models.Entitys;
using DBH.Models.EntityViews;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        /// <returns></returns>
        public async Task<IActionResult> Search()
        {
            return View();
        }

        #endregion

        #region AjaxRequest的处理
        public async Task<IActionResult> DBServcieSave()
        {
            object obj = new { message = "success", code = "100" };
            return Json(obj);
        }
        #endregion
    }
}
