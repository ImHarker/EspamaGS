using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EspamaGS_2._0.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Controllers {
    [Authorize]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly EspamaGSContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, EspamaGSContext context, SignInManager<IdentityUser> signInManager) {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Index() {
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            if (_signInManager.IsSignedIn(User))
                TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            return View(_context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList());
        }


        public IActionResult Favoritos() {
            var categoriasEscolhidas = _context.Preferencia.Where(x => x.IdCliente == User.Identity!.Name)
                .Include(x => x.IdCategoriaNavigation);
            ViewData["Categorias"] = _context.Categoria.Where(x => categoriasEscolhidas.FirstOrDefault(c => x.Id == c.IdCategoriaNavigation.Id) == null);
            return View(categoriasEscolhidas.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Favoritos(int? id) {
            if (id == null) return RedirectToAction(nameof(Favoritos));
            Preferencia pref = new Preferencia {
                IdCliente = User.Identity!.Name!,
                IdCategoria = (int)id
            };
            _context.Preferencia.Add(pref);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Favoritos));
        }

        public async Task<IActionResult> DeleteFavorito(int? id) {
            if (id == null) return RedirectToAction(nameof(Favoritos));
            Preferencia? pref;
            if ((pref = _context.Preferencia.FirstOrDefault(c => c.IdCategoria == id && c.IdCliente == User.Identity!.Name)) == null) return RedirectToAction(nameof(Favoritos));
            _context.Preferencia.Remove(pref);
            await _context.SaveChangesAsync();
            TempData["msg"] = "Removido das categorias preferidas com sucesso!";
            return RedirectToAction(nameof(Favoritos));
        }

        public IActionResult Settings() {

            return View();
        }


        public IActionResult Carrinho() {
            return View(_context.Carts.Include(c => c.IdJogoNavigation).Include(c => c.IdJogoNavigation.IdCategoriaNavigation).Include(c => c.IdJogoNavigation.IdDesenvolvedoraNavigation).Include(c => c.IdJogoNavigation.IdPlataformaNavigation).Where(c => c.IdCliente == User.Identity!.Name).ToList());
        }

        public async Task<IActionResult> AddCarrinho(int? id) {
            if (id == null) return RedirectToAction(nameof(Index));
            if (_context.Jogos.FirstOrDefault(x => x.Id == id) == null) return RedirectToAction(nameof(Index));
            _context.Carts.Add(new Cart {
                IdCliente = User.Identity!.Name!,
                IdJogo = (int)id,
            });
            await _context.SaveChangesAsync();
            TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            TempData["msg"] = "Adicionado ao carrinho com sucesso!";
            return View(nameof(Jogo), _context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).FirstOrDefault(x => x.Id == id));
        }

        public async Task<IActionResult> DeleteCarrinho(int? id) {
            if (id == null) return RedirectToAction(nameof(Index));
            Cart? cartitem;
            if ((cartitem = _context.Carts.FirstOrDefault(c=> c.Id == id)) == null) return RedirectToAction(nameof(Carrinho));
            _context.Carts.Remove(cartitem);
            await _context.SaveChangesAsync();
            TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            TempData["msg"] = "Removido do carrinho com sucesso!";
            return RedirectToAction(nameof(Carrinho));
        }

        [AllowAnonymous]
        public IActionResult Jogo(int? id) {
            if (id == null) return RedirectToAction(nameof(Index));
            Jogo? jogo;
            if ((jogo = _context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdDesenvolvedoraNavigation).Include(c => c.IdPlataformaNavigation).FirstOrDefault(x => x.Id == id)) == null) return RedirectToAction(nameof(Index));
            return View(jogo);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}