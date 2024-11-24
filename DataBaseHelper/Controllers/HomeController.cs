using DataBaseHelper.Common;
using DataBaseHelper.Models;
using DBH.BLLServiceProvider.MainBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataBaseHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBHManagerBLLProvider _DBHManagerBLLProvider;
        private readonly JWTHelper _jwtHelper;

        public HomeController(ILogger<HomeController> logger, IDBHManagerBLLProvider dBHManagerBLLProvider, JWTHelper jWTHelper)//
        {
            _logger = logger;
            _DBHManagerBLLProvider = dBHManagerBLLProvider;
            _jwtHelper = jWTHelper;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetToken()
        {
            return Json(new { token = _jwtHelper.CreateToken() });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}