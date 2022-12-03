using EspamaGS_2._0.Data;
using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspamaGS_2._0.Controllers {
    [Authorize(Roles = "Admin, Funcionario")]
    public class AdminPanelController : Controller {
        private readonly EspamaGSContext _context;
        private readonly IHostEnvironment _he;
        public AdminPanelController(EspamaGSContext context, IHostEnvironment he) {
            _context = context;
            _he = he;
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJogo(Jogo jogo, IFormFile foto) {
            if (foto == null) return View(jogo);
            jogo.Foto = "";
            string dest = Path.Combine(_he.ContentRootPath + "/wwwroot/img/Jogos/");
            Directory.CreateDirectory(dest);
            jogo.IdFuncionario = "a";
            jogo.DataRegisto = DateTime.Now;
            jogo.DataLancamento = DateTime.Today;
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            jogo.Foto = jogo.Id.ToString() + "." + foto.FileName.Split(".").Last();
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
            dest = Path.Combine(dest, jogo.Foto);
            FileStream fs = new FileStream(dest, FileMode.Create);
            foto.CopyTo(fs);
            fs.Close();
            return View();
        }

        public IActionResult Dashboard() {
            return View();
        }
    }
}
