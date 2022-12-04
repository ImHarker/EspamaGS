using EspamaGS_2._0.Data;
using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Controllers {
    [Authorize(Roles = "Admin, Funcionario")]
    public class AdminPanelController : Controller {
        private readonly EspamaGSContext _context;
        private readonly IHostEnvironment _he;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminPanelController(EspamaGSContext context, IHostEnvironment he, UserManager<IdentityUser> userManager) {
            _context = context;
            _he = he;
            _userManager = userManager;
        }


        public IActionResult Index() {

            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult LogsCompras() {
            return View();
        }
        public IActionResult LogsFuncionarios() {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult LogsAdmins() {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddFuncionarios() {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmins() {
            return View();
        }


        public IActionResult AddCategoria() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoria(Categoria c) {
            _context.Categoria.Add(c);
            await _context.SaveChangesAsync();
            return View();
        }

        public IActionResult AddPlataforma() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPlataforma(Plataforma p) {
            _context.Plataformas.Add(p);
            await _context.SaveChangesAsync();
            return View();
        }

        public IActionResult AddDesenvolvedora() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDesenvolvedora(Desenvolvedora d) {
            _context.Desenvolvedoras.Add(d);
            await _context.SaveChangesAsync();
            return View();
        }


        public IActionResult AddJogo() {
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            ViewData["Desenvolvedoras"] = _context.Desenvolvedoras.ToList();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJogo(Jogo jogo, IFormFile foto) {
            if (foto == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Nome == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Descricao == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Preco == -1) return RedirectToAction(nameof(AddJogo));
            if (jogo.DataLancamento == null) return RedirectToAction(nameof(AddJogo));
            jogo.Foto = "";
            string dest = Path.Combine(_he.ContentRootPath + "/wwwroot/img/Jogos/");
            Directory.CreateDirectory(dest);
            jogo.IdFuncionario = User.Identity.Name;
            jogo.DataRegisto = DateTime.Now;
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            jogo.Foto = jogo.Id.ToString() + "." + foto.FileName.Split(".").Last();
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
            dest = Path.Combine(dest, jogo.Foto);
            FileStream fs = new FileStream(dest, FileMode.Create);
            foto.CopyTo(fs);
            fs.Close();
            TempData["msg"] = "O jogo '" + jogo.Nome + "' foi inserido com sucesso!";
            return RedirectToAction(nameof(AddJogo));
        }

        public IActionResult Dashboard() {
            ViewData["Users"] = _userManager.Users.ToList();
            return View();
        }
    }
}
