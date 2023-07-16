using DataBaseHelper.Models;
using DBH.BLLServiceProvider.MainBLL;
using DBH.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataBaseHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBHManagerBLLProvider _DBHManagerBLLProvider;
        public HomeController(ILogger<HomeController> logger, IDBHManagerBLLProvider dBHManagerBLLProvider)//
        {
            _logger = logger;
            _DBHManagerBLLProvider = dBHManagerBLLProvider;
        }

        public async Task<IActionResult> Index()
        {
            FS_ServicesEntity fS_ServicesEntity = new FS_ServicesEntity();
            fS_ServicesEntity = await _DBHManagerBLLProvider.TestAsync(1);
            _logger.LogDebug("Debug : Home --> Index");
            return View(fS_ServicesEntity);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}