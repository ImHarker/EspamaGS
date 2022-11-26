using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EspamaGS_2._0.Data;

namespace EspamaGS_2._0.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly EspamaGSContext _context;

        public HomeController(ILogger<HomeController> logger, EspamaGSContext context) {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() {
            TempData["cartitems"] = 100;
            return View();
        }

        public IActionResult Login() {
            if (TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpPost]
        public IActionResult Login(Utilizador user) {
            if (_context.Utilizadors.Any(x => x.Username == user.Username && x.Passwd == user.Passwd)) {
                TempData["username"] = user.Username;
                return RedirectToAction(nameof(Index));
            }

            TempData["errormsg"] = "Credenciais Inválidas";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register() {
            if (TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpPost]
        public IActionResult Register(Utilizador user) {

            _logger.LogCritical("Register - Not Implemented!");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Favoritos() {

            return View();
        }

        public IActionResult Settings() {

            return View();
        }

        public IActionResult LogOut() {
            TempData.Remove("username");
            return RedirectToAction(nameof(Index));
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