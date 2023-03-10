using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EspamaGS_2._0.Data;
using EspamaGS_2._0.Services;
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

        public async Task<IActionResult> ApagarReview(int? id) {
            var review = _context.Reviews.FirstOrDefault(c => c.Id == id);
            if (review == null) return RedirectToAction(nameof(Index));
            if(!User.IsInRole("Admin")) return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            TempData["msg"] = $"A Crítica de '{review.IdCliente}' foi apagada com sucesso!";
            return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review) {
            if(! _context.Compras.Any(x => x.IdJogo == review.IdJogo && x.IdCliente == User.Identity!.Name)) return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
            if (String.IsNullOrWhiteSpace(review.ReviewMessage)) {
                TempData["msgerr"] = "A sua crítica deve estar preenchida!";
                return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
            }
            if (review.Rating <= 0) {
                TempData["msgerr"] = "A sua crítica deve conter um rating!";
                return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
            }
            var reviewupdate = _context.Reviews.FirstOrDefault(x => x.IdJogo == review.IdJogo && x.IdCliente == User.Identity!.Name);
            if (reviewupdate != null) {
                reviewupdate.ReviewMessage = review.ReviewMessage;
                reviewupdate.Rating = review.Rating;
                reviewupdate.DataReview = review.DataReview;
                _context.Reviews.Update(reviewupdate);
            } else {
                review.DataReview = DateTime.Now;
                _context.Reviews.Add(review);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Jogo), new { id = review.IdJogo });
        }

        [AllowAnonymous]
        public IActionResult Scroll(int? page) {
            if (page == null) return NoContent();
            return Json(_context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).Skip((int)page * 10).Take(10).ToList());

        }

        [AllowAnonymous]
        public IActionResult Pesquisa(string? nome, int? cat, int? plat, int? ordenar, int? ordem) {
            IQueryable<Jogo> result;
            if (string.IsNullOrEmpty(nome)) nome = "";

            result = _context.Jogos.Where(c => c.Nome.Contains(nome));

            if (cat != 0) {
                result = result.Where(c => c.IdCategoria == cat);
            }

            if (plat != 0) {
                result = result.Where(c => c.IdPlataforma == plat);
            }

            switch (ordenar) {
                case 0:
                    return Json(ordem == 0 ? result.OrderByDescending(c => _context.Compras.Count(j => j.IdJogo == c.Id)).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList() : result.OrderBy(c => _context.Compras.Count(j => j.IdJogo == c.Id)).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList());
                case 1:
                    return Json(ordem == 0 ? result.OrderByDescending(c => c.Preco).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList() : result.OrderBy(c => c.Preco).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList());
                case 2:
                    return Json(ordem == 0 ? result.OrderByDescending(c => c.DataLancamento).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList() : result.OrderBy(c => c.DataLancamento).Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).ToList());
            }

            return NoContent();
        }

        [AllowAnonymous]
        public IActionResult Index() {
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            if (_signInManager.IsSignedIn(User))
                TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            return View(_context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdPlataformaNavigation).Include(c => c.IdDesenvolvedoraNavigation).Take(10).ToList());
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
            TempData["msg"] = "Adicionado às categorias preferidas com sucesso!";
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

            return View(_context.UserSettings.FirstOrDefault(c => c.Id == User.Identity!.Name));
        }

        [HttpPost]
        public async Task<IActionResult> Settings(int? id) {
            if (id == null) return NoContent();
            var usr = _context.UserSettings.FirstOrDefault(c => c.Id == User.Identity!.Name);
            if (usr == null) return NoContent();
            switch (id) {
                case 0:
                    usr.EmailNotifications = !usr.EmailNotifications;
                    break;
            }

            _context.UserSettings.Update(usr);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public IActionResult Carrinho() {
            return View(_context.Carts.Include(c => c.IdJogoNavigation).Include(c => c.IdJogoNavigation.IdCategoriaNavigation).Include(c => c.IdJogoNavigation.IdDesenvolvedoraNavigation).Include(c => c.IdJogoNavigation.IdPlataformaNavigation).Where(c => c.IdCliente == User.Identity!.Name).ToList());
        }

        public async Task<IActionResult> BuyNow(int id) {
            if (id == null) return RedirectToAction(nameof(Index));
            if (_context.Jogos.FirstOrDefault(x => x.Id == id) == null) return RedirectToAction(nameof(Index));
            _context.Carts.Add(new Cart {
                IdCliente = User.Identity!.Name!,
                IdJogo = (int)id,
            });
            await _context.SaveChangesAsync();
            var items = new List<Compra>();
            var cartitem = _context.Carts.Include(c => c.IdJogoNavigation).FirstOrDefault(c => c.IdCliente == User.Identity!.Name && c.IdJogo == id);
            if (cartitem == null) return RedirectToAction(nameof(Carrinho));
            var compra = new Compra {
                DataCompra = DateTime.Now,
                Preco = cartitem.IdJogoNavigation.Preco,
                IdCliente = cartitem.IdCliente,
                IdJogo = cartitem.IdJogoNavigation.Id,
                Key = GameKeyGenerator.GenerateKey()
            };
            items.Add(compra);
            _context.Carts.Remove(cartitem);

            _context.Compras.AddRange(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Compras));
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
            if ((cartitem = _context.Carts.FirstOrDefault(c => c.Id == id)) == null) return RedirectToAction(nameof(Carrinho));
            _context.Carts.Remove(cartitem);
            await _context.SaveChangesAsync();
            TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            TempData["msg"] = "Removido do carrinho com sucesso!";
            return RedirectToAction(nameof(Carrinho));
        }

        public async Task<IActionResult> Checkout() {
            var items = new List<Compra>();
            var cartitems = _context.Carts.Where(c => c.IdCliente == User.Identity!.Name).Include(c => c.IdJogoNavigation).ToList();
            if (!cartitems.Any()) return RedirectToAction(nameof(Carrinho));
            foreach (var item in cartitems) {
                var compra = new Compra {
                    DataCompra = DateTime.Now,
                    Preco = item.IdJogoNavigation.Preco,
                    IdCliente = item.IdCliente,
                    IdJogo = item.IdJogoNavigation.Id,
                    Key = GameKeyGenerator.GenerateKey()
                };
                items.Add(compra);
                _context.Carts.Remove(item);
            }
            _context.Compras.AddRange(items);
            await _context.SaveChangesAsync();
            TempData["cartitems"] = _context.Carts.Count(c => c.IdCliente == User.Identity!.Name);
            return RedirectToAction(nameof(Compras));
        }

        public IActionResult Compras() {

            return View(_context.Compras.Where(c => c.IdCliente == User.Identity!.Name).Include(c => c.IdJogoNavigation).Include(c => c.IdJogoNavigation.IdCategoriaNavigation).Include(c => c.IdJogoNavigation.IdDesenvolvedoraNavigation).Include(c => c.IdJogoNavigation.IdPlataformaNavigation).OrderByDescending(c => c.DataCompra).ToList());
        }

        [AllowAnonymous]
        public IActionResult Jogo(int? id) {
            if (id == null) return RedirectToAction(nameof(Index));
            Jogo? jogo;
            if ((jogo = _context.Jogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdDesenvolvedoraNavigation).Include(c => c.IdPlataformaNavigation).FirstOrDefault(x => x.Id == id)) == null) return RedirectToAction(nameof(Index));
            ViewData["reviews"] = _context.Reviews.Where(x => x.IdJogo == id).ToList();
            ViewData["comprado"] = _context.Compras.Any(x => x.IdJogo == id && x.IdCliente == User.Identity!.Name);
            var review = _context.Reviews.FirstOrDefault(x => x.IdJogo == id && x.IdCliente == User.Identity!.Name);
            ViewData["userReview"] = review ?? new Review { IdCliente = User.Identity!.Name!, IdJogo = (int)id };
            return View(jogo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}