using EspamaGS_2._0.Data;
using EspamaGS_2._0.Models;
using EspamaGS_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace EspamaGS_2._0.Controllers {
    [Authorize(Roles = "Admin, Funcionario")]
    public class AdminPanelController : Controller {
        private readonly EspamaGSContext _context;
        private readonly IHostEnvironment _he;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        public AdminPanelController(EspamaGSContext context, IHostEnvironment he, UserManager<IdentityUser> userManager, IEmailSender emailSender) {
            _context = context;
            _he = he;
            _userManager = userManager;
            _emailSender = emailSender;
        }


        public IActionResult Index() {

            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult LogsCompras() {
            return View();
        }
        public IActionResult LogsJogos() {
            return View(_context.Jogos.Include(c => c.IdFuncionarioNavigation).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult LogsFuncionarios() {
            return View(_context.Funcionarios.Include(c => c.IdAdminNavigation).ToList());
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
            if (_context.Categoria.Any(x => x.Nome == c.Nome)) return RedirectToAction(nameof(AddCategoria));
            _context.Categoria.Add(c);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A categoria '" + c.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddCategoria));
        }

        public IActionResult AddPlataforma() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPlataforma(Plataforma p) {
            if (_context.Plataformas.Any(x => x.Nome == p.Nome)) return RedirectToAction(nameof(AddPlataforma));

            _context.Plataformas.Add(p);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A plataforma '" + p.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddPlataforma));
        }

        public IActionResult AddDesenvolvedora() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDesenvolvedora(Desenvolvedora d) {
            if (_context.Desenvolvedoras.Any(x => x.Nome == d.Nome)) return RedirectToAction(nameof(AddDesenvolvedora));

            _context.Desenvolvedoras.Add(d);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A desenvolvedora '" + d.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddDesenvolvedora));
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
            jogo.IdFuncionario = User.Identity!.Name!;
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
            foreach (var user in _userManager.Users) {
                if (_context.Preferencia.Any(c => user.UserName == c.IdCliente && c.IdCategoria == jogo.IdCategoria)) {
                    var callbackUrl = Url.ActionLink("Jogo", "Home", values: new { id = jogo.Id });
                    await _emailSender.SendEmailAsync(user.Email, "Novo Jogo numa Categoria Preferida", $"O jogo '{jogo.Nome}' foi adicionado e está numa das suas categorias preferidas.\nPode ser visitado aqui: <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>{jogo.Nome}</a>");
                }
            }

            return RedirectToAction(nameof(AddJogo));
        }

        public IActionResult Dashboard() {
            ViewData["Users"] = _userManager.Users.ToList();
            return View();
        }
    }
}
