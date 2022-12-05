using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EspamaGS_2._0.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            TempData["cartitems"] = 100;
            return View(_context.Jogos.Include(c=> c.IdCategoriaNavigation).Include(c=> c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList());
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

        public IActionResult Jogo(int? id) {
            if (id == null) return RedirectToAction(nameof(Index));
            Jogo? jogo;
            if((jogo = _context.Jogos.Include(c=> c.IdCategoriaNavigation).Include(c=>c.IdDesenvolvedoraNavigation).Include(c=> c.IdPlataformaNavigation).FirstOrDefault(x => x.Id == id)) == null) return RedirectToAction(nameof(Index));
            return View(jogo);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}