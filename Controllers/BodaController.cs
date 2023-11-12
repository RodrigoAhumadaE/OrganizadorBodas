using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizadorBodas.Models;

namespace OrganizadorBodas.Controllers;

public class BodaController : Controller{
    private readonly ILogger<BodaController> _logger;
    private MyContext _context;

    public BodaController(ILogger<BodaController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    // GET
    [HttpGet("bodas")]
    [SessionCheck]
    public IActionResult Bodas(){
        Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("email"));
        HttpContext.Session.SetString("nombreUsuario",$"{usuario.Nombre} {usuario.Apellido}");
        HttpContext.Session.SetInt32("id", (Int32)usuario.UsuarioId);
        EliminaBodasCaducas();
        List<Boda> ListaBodas = _context.Bodas.Include(p => p.ListaParticipaciones).ThenInclude(p => p.Usuario).ToList();
        return View("Bodas", ListaBodas);
    }

    [HttpGet("nueva/boda")]
    [SessionCheck]
    public IActionResult NuevaBoda(){
        return View("NuevaBoda");
    }

    [HttpGet("boda/{BodaId}")]
    [SessionCheck]
    public IActionResult DetalleBoda(int bodaId){
        Boda? boda = _context.Bodas.Include(b => b.ListaParticipaciones).ThenInclude(b => b.Usuario).FirstOrDefault(b => b.BodaId == bodaId);
        return View("DetalleBoda", boda);
    }



    // POST
    [HttpPost("procesa/boda")]
    public IActionResult ProcesaBoda(Boda boda){
        if(ModelState.IsValid){
            _context.Bodas.Add(boda);
            _context.SaveChanges();
            Boda? bodaBuscar = _context.Bodas.FirstOrDefault(b => b.Novia == boda.Novia && b.Novio == boda.Novio && b.Fecha == boda.Fecha && b.Direccion == boda.Direccion);
            if(bodaBuscar != null){
                return Redirect($"../boda/{bodaBuscar.BodaId}");
            }
            return RedirectToAction("Bodas");
        }
        return View("NuevaBoda");
    }

    [HttpGet("eliminar/boda/{BodaId}")]
    public IActionResult EliminarBoda(int bodaId){
        Boda? bodaBorrar = _context.Bodas.SingleOrDefault(b => b.BodaId == bodaId);
        if(bodaBorrar != null){
            _context.Bodas.Remove(bodaBorrar);
            _context.SaveChanges();
            return RedirectToAction("Bodas");
        }
        return View("Bodas");
    }

    public void EliminaBodasCaducas(){
        List<Boda>? ListaBodaCaducada = _context.Bodas.Where(b => b.Fecha.Date == DateTime.Now.Date).ToList();
        if(ListaBodaCaducada != null){
            foreach(Boda boda in ListaBodaCaducada){
                _context.Bodas.Remove(boda);
                _context.SaveChanges();
            }
        }
    }
}