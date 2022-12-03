using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EspamaGS_2._0.Data;
using Microsoft.AspNetCore.Authorization;

namespace EspamaGS_2._0.Controllers {
    [Authorize]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly EspamaGSContext _context;

        public HomeController(ILogger<HomeController> logger, EspamaGSContext context) {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index() {
            TempData["cartitems"] = 100;
            return View();
        }


        public IActionResult Favoritos() {

            return View();
        }

        public IActionResult Settings() {

            return View();
        }

         
        public IActionResult Checkout() {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}