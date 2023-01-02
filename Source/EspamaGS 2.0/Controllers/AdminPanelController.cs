using EspamaGS_2._0.Data;
using EspamaGS_2._0.Models;
using EspamaGS_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
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

        #region Logs

        public IActionResult LogsCompras() {
            return View(_context.Compras.Include(c => c.IdJogoNavigation).ToList());
        }
        public IActionResult LogsJogos() {
            return View(_context.Jogos.Include(c => c.IdFuncionarioNavigation).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult LogsFuncionarios() {
            ViewData["Funcionarios"] = _context.Funcionarios.Include(c => c.IdAdminNavigation).ToList();
            ViewData["Admin"] = _context.Administradors.Include(c => c.IdAdminNavigation).ToList();

            return View();
        }
        #endregion

        #region Add    

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFuncionarios(string? id, int? tipo, string? tlf) {
            if (id == null) return RedirectToAction(nameof(AddFuncionarios));
            if (tipo is not (1 or 2)) return RedirectToAction(nameof(AddFuncionarios));
            if (tipo == 1 && tlf == null) return RedirectToAction(nameof(AddFuncionarios));

            var user = _userManager.Users.FirstOrDefault(c => c.UserName == id);
            if (user == null) return RedirectToAction(nameof(AddFuncionarios));
            if (_userManager.IsInRoleAsync(user, "Admin").Result || _userManager.IsInRoleAsync(user, "Funcionario").Result)
                return RedirectToAction(nameof(AddFuncionarios));

            if (tipo == 1) {
                await _userManager.AddToRoleAsync(user, "Funcionario");
                _context.Funcionarios.Add(new Funcionario {
                    DataRegisto = DateTime.Now,
                    IdAdmin = User.Identity!.Name!,
                    IdUtilizador = user.UserName,
                    Telefone = tlf!
                });
                await _context.SaveChangesAsync();
                TempData["msg"] = "O Utilizador '" + id + "' foi promovido a 'Funcionário'!";
            }

            if (tipo == 2) {
                await _userManager.AddToRoleAsync(user, "Admin");
                _context.Administradors.Add(new Administrador {
                    DataRegisto = DateTime.Now,
                    IdAdmin = User.Identity!.Name!,
                    IdUtilizador = user.UserName
                });
                await _context.SaveChangesAsync();
                TempData["msg"] = "O Utilizador '" + id + "' foi promovido a 'Administrador'!";
            }


            return RedirectToAction(nameof(AddFuncionarios));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddFuncionarios() {
            var funcionarios = _context.Funcionarios;
            var administradores = _context.Administradors;
            var users = _userManager.Users.Where(x => funcionarios.FirstOrDefault(c => x.UserName == c.IdUtilizador) == null).Where(x => administradores.FirstOrDefault(c => x.UserName == c.IdUtilizador) == null).ToList();


            return View(users);
        }

        [Authorize(Roles = "Funcionario")]

        public IActionResult AddCategoria() {
            return View();
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddCategoria(Categoria c) {
            if (_context.Categoria.Any(x => x.Nome == c.Nome)) return RedirectToAction(nameof(AddCategoria));
            _context.Categoria.Add(c);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A categoria '" + c.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddCategoria));
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult AddPlataforma() {
            return View();
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddPlataforma(Plataforma p) {
            if (_context.Plataformas.Any(x => x.Nome == p.Nome)) return RedirectToAction(nameof(AddPlataforma));

            _context.Plataformas.Add(p);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A plataforma '" + p.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddPlataforma));
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult AddDesenvolvedora() {
            return View();
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddDesenvolvedora(Desenvolvedora d) {
            if (_context.Desenvolvedoras.Any(x => x.Nome == d.Nome)) return RedirectToAction(nameof(AddDesenvolvedora));

            _context.Desenvolvedoras.Add(d);
            await _context.SaveChangesAsync();
            TempData["msg"] = "A desenvolvedora '" + d.Nome + "' foi inserida com sucesso!";

            return RedirectToAction(nameof(AddDesenvolvedora));
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult AddJogo() {
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            ViewData["Desenvolvedoras"] = _context.Desenvolvedoras.ToList();


            return View();
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public async Task<IActionResult> AddJogo(Jogo jogo, IFormFile foto) {
            if (foto == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Nome == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Descricao == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.Preco < 0) return RedirectToAction(nameof(AddJogo));
            if (jogo.DataLancamento == null) return RedirectToAction(nameof(AddJogo));
            if (jogo.IdCategoria == 0) return RedirectToAction(nameof(AddJogo));
            if (jogo.IdPlataforma == 0) return RedirectToAction(nameof(AddJogo));
            if (jogo.IdDesenvolvedora == 0) return RedirectToAction(nameof(AddJogo));
            jogo.Foto = "";
            jogo.IdFuncionario = User.Identity!.Name!;
            string dest = Path.Combine(_he.ContentRootPath + "/wwwroot/img/Jogos/");
            Directory.CreateDirectory(dest);
            jogo.DataRegisto = DateTime.Now;
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            jogo.Foto = jogo.Id + "." + foto.FileName.Split(".").Last();
            _context.Jogos.Update(jogo);
            await _context.SaveChangesAsync();
            dest = Path.Combine(dest, jogo.Foto);
            FileStream fs = new FileStream(dest, FileMode.Create);
            foto.CopyTo(fs);
            fs.Close();
            TempData["msg"] = "O jogo '" + jogo.Nome + "' foi inserido com sucesso!";
            foreach (var user in _userManager.Users) {
                if(_context.UserSettings.FirstOrDefault(c => c.Id == user.UserName).EmailNotifications)
                if (_context.Preferencia.Any(c => user.UserName == c.IdCliente && c.IdCategoria == jogo.IdCategoria)) {
                    var callbackUrl = Url.ActionLink("Jogo", "Home", values: new { id = jogo.Id });
                    await _emailSender.SendEmailAsync(user.Email, "Novo Jogo numa Categoria Preferida", $"O jogo '{jogo.Nome}' foi adicionado e está numa das suas categorias preferidas.\nPode ser visitado aqui: <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>{jogo.Nome}</a>");
                }
            }

            return RedirectToAction(nameof(AddJogo));
        }

        #endregion

        #region Gerir
        [Authorize(Roles = "Admin")]

        public IActionResult GerirFuncionarios() {
            ViewData["Funcionarios"] = _context.Funcionarios.Where(c => c.IdAdmin == User.Identity!.Name).ToList();
            ViewData["Admin"] = _context.Administradors.Where(c => c.IdAdmin == User.Identity!.Name).ToList();
            return View();
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult GerirJogos() {

            return View(_context.Jogos.Include(c => c.IdPlataformaNavigation).Where(c => c.IdFuncionario == User.Identity!.Name).ToList());
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult GerirCategorias() {

            return View(_context.Categoria.ToList());
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult GerirPlataformas() {

            return View(_context.Plataformas.ToList());
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult GerirDesenvolvedoras() {

            return View(_context.Desenvolvedoras.ToList());
        }
        #endregion

        #region Editar
        [Authorize(Roles = "Admin")]

        public IActionResult EditarFuncionario(string? id) {
            var func = _context.Funcionarios.FirstOrDefault(c => c.IdUtilizador == id);
            if (func == null) { return RedirectToAction(nameof(GerirJogos)); }
            return View(func);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> EditarFuncionario(string? id, Funcionario fun) {
            var funupdate = _context.Funcionarios.FirstOrDefault(c => c.IdUtilizador == id);
            if (funupdate == null) return RedirectToAction(nameof(GerirFuncionarios));
            if (String.IsNullOrEmpty(fun.Telefone)) return View(funupdate);
            funupdate.Telefone = fun.Telefone;
            _context.Funcionarios.Update(funupdate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirFuncionarios));
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult EditarJogo(int? id) {
            var jogo = _context.Jogos.FirstOrDefault(c => c.Id == id);
            if (jogo == null) { return RedirectToAction(nameof(GerirJogos)); }
            ViewData["Categorias"] = _context.Categoria.ToList();
            ViewData["Plataformas"] = _context.Plataformas.ToList();
            ViewData["Desenvolvedoras"] = _context.Desenvolvedoras.ToList();
            return View(jogo);
        }
        [Authorize(Roles = "Funcionario")]

        [HttpPost]
        public async Task<IActionResult> EditarJogo(int? id, Jogo jogo, IFormFile? foto) {
            var jogoupdate = _context.Jogos.FirstOrDefault(c => c.Id == id);
            if (jogoupdate == null) { return RedirectToAction(nameof(GerirJogos)); }
            if (String.IsNullOrEmpty(jogo.Nome)) return RedirectToAction(nameof(EditarJogo), id);
            if (String.IsNullOrEmpty(jogo.Descricao)) return RedirectToAction(nameof(EditarJogo), id);
            if (jogo.Preco < 0) return RedirectToAction(nameof(EditarJogo), id);
            if (jogo.DataLancamento == null) return RedirectToAction(nameof(EditarJogo), id);
            if (jogo.IdCategoria == 0) return RedirectToAction(nameof(EditarJogo), id);
            if (jogo.IdPlataforma == 0) return RedirectToAction(nameof(EditarJogo), id);
            if (jogo.IdDesenvolvedora == 0) return RedirectToAction(nameof(EditarJogo), id);
            jogoupdate.Nome = jogo.Nome;
            jogoupdate.Descricao = jogo.Descricao;
            jogoupdate.Preco = jogo.Preco;
            jogoupdate.DataLancamento = jogo.DataLancamento;
            jogoupdate.IdCategoria = jogo.IdCategoria;
            jogoupdate.IdPlataforma = jogo.IdPlataforma;
            jogoupdate.IdDesenvolvedora = jogo.IdDesenvolvedora;

            if (foto != null) {
                string dest = Path.Combine(_he.ContentRootPath + "/wwwroot/img/Jogos/");
                if (System.IO.File.Exists(Path.Combine(dest, jogoupdate.Foto))) {
                    System.IO.File.Delete(Path.Combine(dest, jogoupdate.Foto));
                }
                jogoupdate.Foto = jogoupdate.Id + "." + foto.FileName.Split(".").Last();
                dest = Path.Combine(dest, jogoupdate.Foto);
                FileStream fs = new FileStream(dest, FileMode.Create);
                await foto.CopyToAsync(fs);
                fs.Close();
            }

            _context.Jogos.Update(jogoupdate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirJogos));
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult EditarCategoria(int? id) {
            var cat = _context.Categoria.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (cat == null) return RedirectToAction(nameof(GerirCategorias));
            return View(cat);
        }
        [Authorize(Roles = "Funcionario")]

        [HttpPost]
        public async Task<IActionResult> EditarCategoria(int? id, Categoria cat) {
            if (id != cat.Id) return RedirectToAction(nameof(GerirCategorias));
            try {
                _context.Categoria.Update(cat);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (_context.Categoria.Any(c => c.Id == id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(GerirCategorias)); ;
        }

        [Authorize(Roles = "Funcionario")]

        public IActionResult EditarPlataforma(int? id) {
            var plat = _context.Plataformas.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (plat == null) return RedirectToAction(nameof(GerirPlataformas));
            return View(plat);
        }
        [Authorize(Roles = "Funcionario")]

        [HttpPost]
        public async Task<IActionResult> EditarPlataforma(int? id, Plataforma plat) {
            if (id != plat.Id) return RedirectToAction(nameof(GerirPlataformas));
            try {
                _context.Plataformas.Update(plat);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (_context.Plataformas.Any(c => c.Id == id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(GerirPlataformas)); ;
        }
        [Authorize(Roles = "Funcionario")]

        public IActionResult EditarDesenvolvedora(int? id) {
            var des = _context.Desenvolvedoras.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (des == null) return RedirectToAction(nameof(GerirDesenvolvedoras));
            return View(des);
        }
        [Authorize(Roles = "Funcionario")]

        [HttpPost]
        public async Task<IActionResult> EditarDesenvolvedora(int? id, Desenvolvedora des) {
            if (id != des.Id) return RedirectToAction(nameof(GerirDesenvolvedoras));
            try {
                _context.Desenvolvedoras.Update(des);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (_context.Desenvolvedoras.Any(c => c.Id == id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(GerirDesenvolvedoras)); ;
        }

        #endregion

        #region Apagar
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ApagarFuncionario(string? id) {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == id);
            if (user == null) { return RedirectToAction(nameof(GerirFuncionarios)); }
            var func = _context.Funcionarios.FirstOrDefault(c => c.IdUtilizador == id);
            if (func == null) { return RedirectToAction(nameof(GerirFuncionarios)); }
            if (_context.Jogos.Include(c => c.IdFuncionarioNavigation).Any(c => c.IdFuncionario == id)) { return RedirectToAction(nameof(GerirFuncionarios)); }
            _context.Funcionarios.Remove(func);
            await _userManager.RemoveFromRoleAsync(user, "Funcionario");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirFuncionarios));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ApagarAdministrador(string? id) {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == id);
            if (user == null) { return RedirectToAction(nameof(GerirFuncionarios)); }

            var admin = _context.Administradors.FirstOrDefault(c => c.IdUtilizador == id);
            if (admin == null) { return RedirectToAction(nameof(GerirFuncionarios)); }
            if (_context.Funcionarios.Include(c => c.IdAdminNavigation).Any(c => c.IdAdmin == id)) { return RedirectToAction(nameof(GerirFuncionarios)); }
            if (_context.Administradors.Include(c => c.IdAdminNavigation).Any(c => c.IdAdmin == id)) { return RedirectToAction(nameof(GerirFuncionarios)); }

            _context.Administradors.Remove(admin);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GerirFuncionarios));
        }
        [Authorize(Roles = "Funcionario")]

        public async Task<IActionResult> ApagarJogo(int? id) {
            string dest = Path.Combine(_he.ContentRootPath + "/wwwroot/img/Jogos/");
            var jogo = _context.Jogos.FirstOrDefault(c => c.Id == id);
            if (jogo == null) { return RedirectToAction(nameof(GerirJogos)); }
            dest = Path.Combine(dest, jogo.Foto);
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();

            if (System.IO.File.Exists(dest)) {
                System.IO.File.Delete(dest);
            }

            return RedirectToAction(nameof(GerirJogos));
        }
        [Authorize(Roles = "Funcionario")]

        public async Task<IActionResult> ApagarCategoria(int? id) {
            var cat = _context.Categoria.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (cat == null) return RedirectToAction(nameof(GerirCategorias));
            if (cat.Jogos.Count != 0) return RedirectToAction(nameof(GerirCategorias));
            _context.Categoria.Remove(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirCategorias));
        }
        [Authorize(Roles = "Funcionario")]

        public async Task<IActionResult> ApagarPlataforma(int? id) {
            var plat = _context.Plataformas.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (plat == null) return RedirectToAction(nameof(GerirPlataformas));
            if (plat.Jogos.Count != 0) return RedirectToAction(nameof(GerirPlataformas));
            _context.Plataformas.Remove(plat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirPlataformas));
        }
        [Authorize(Roles = "Funcionario")]

        public async Task<IActionResult> ApagarDesenvolvedora(int? id) {
            var des = _context.Desenvolvedoras.Include(c => c.Jogos).FirstOrDefault(c => c.Id == id);
            if (des == null) return RedirectToAction(nameof(GerirDesenvolvedoras));
            if (des.Jogos.Count != 0) return RedirectToAction(nameof(GerirDesenvolvedoras));
            _context.Desenvolvedoras.Remove(des);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GerirDesenvolvedoras));
        }

        #endregion

        public IActionResult Dashboard() {
            ViewData["Users"] = _userManager.Users.ToList();
            return View();
        }
    }
}
