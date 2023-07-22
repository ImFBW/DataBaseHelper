using DBH.BLLServiceProvider.MainBLL;
using DBH.Models.Entitys;
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
            IList<FS_ServicesEntity> listEntity = new List<FS_ServicesEntity>();
            listEntity = await _DBHManagerBLLProvider.GetServicesConfigListAsync();
            ViewData["listEntity"] = listEntity;
            return View(listEntity);
        }

        /// <summary>
        /// 视图-弹出框-添加数据库
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> _ViewAddDataBase()
        {
            return View();
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


    }
}
