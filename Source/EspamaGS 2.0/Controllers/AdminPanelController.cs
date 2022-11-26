using EspamaGS_2._0.Data;
using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace EspamaGS_2._0.Controllers {
    public class AdminPanelController : Controller {
        private readonly EspamaGSContext _context;
        public AdminPanelController(EspamaGSContext context) { _context = context; }


        public IActionResult Index() {
            if (TempData.ContainsKey("username")) return RedirectToAction(nameof(Dashboard));

            return View();
        }

        [HttpPost]
        public IActionResult Index(Utilizador user) {
            if (_context.Utilizadors.Any(x => x.Username == user.Username && x.Passwd == user.Passwd)) {
                TempData["username"] = user.Username;
                return RedirectToAction(nameof(Dashboard));
            }

            TempData["errormsg"] = "Credenciais Inválidas";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LogsCompras() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult LogsFuncionarios() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult LogsAdmins() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult AddFuncionarios() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult AddAdmins() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult AddJogo() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult Dashboard() {
            if (!TempData.ContainsKey("username")) return RedirectToAction(nameof(Index));
            return View();
        }
    }
}
