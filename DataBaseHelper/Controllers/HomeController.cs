using DataBaseHelper.Models;
using DBH.BLLServiceProvider.MainBLL;
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
            return View();
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