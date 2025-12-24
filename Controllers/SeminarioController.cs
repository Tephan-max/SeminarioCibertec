using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarioCibertec.Data;
using SeminarioCibertec.Models;
using SeminarioCibertec.Repositorio;

namespace SeminarioCibertec.Controllers
{
    public class SeminarioController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public SeminarioController(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            SeminarioRepository sr = new SeminarioRepository(_appDbContext.Database.GetDbConnection().ConnectionString);
            return View(sr.listarSeminariosConCapacidad());
        }
        public IActionResult Registrar(int id) { 
            var seminario = _appDbContext.Seminario.Find(id);
            if (seminario == null) {
                return RedirectToAction("Index");
            }
            return View(seminario);
        }
        [HttpPost]
        public IActionResult Registrar(int idSeminario, string ? idEstudiante)
        {
            if (idEstudiante == null) {
                return RedirectToAction("Registrar", idSeminario);
            }
            SeminarioRepository sr = new SeminarioRepository(_appDbContext.Database.GetDbConnection().ConnectionString);
            sr.registrarAsistencia(idSeminario, idEstudiante);
            var registro = _appDbContext.RegistroAsistencia.OrderByDescending(i => i.Registro).FirstOrDefault();
            TempData["mensaje"] = "Registro " + registro.Registro + " exitoso";
            return RedirectToAction("Registrar", idSeminario);
        }
    }
}
