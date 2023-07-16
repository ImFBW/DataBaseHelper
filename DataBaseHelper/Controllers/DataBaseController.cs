using Microsoft.AspNetCore.Mvc;

namespace DataBaseHelper.Controllers
{
    /// <summary>
    /// 主要的控制器文件
    /// </summary>
    public class DataBaseController : Controller
    {
        /// <summary>
        /// 首页，选择服务器页面（默认页面）
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 搜索页，主要的搜索功能的页面，可搜索表、存储过程、表值函数
        /// </summary>
        /// <returns></returns>
        public IActionResult Search()
        {
            return View();
        }

    }
}
