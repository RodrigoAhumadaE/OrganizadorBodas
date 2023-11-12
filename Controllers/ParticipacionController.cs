using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizadorBodas.Models;

namespace OrganizadorBodas.Controllers;

public class ParticipacionController : Controller{
    private readonly ILogger<ParticipacionController> _logger;
    private MyContext _context;

    public ParticipacionController(ILogger<ParticipacionController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    // POST
    [HttpPost("procesa/participacion/{BodaId}/{UsuarioId}")]
    public IActionResult ProcesaParticipacion(int bodaId, int usuarioId){
        Participacion partCrear = new Participacion(){
            BodaId = bodaId,
            UsuarioId = (int)usuarioId
        };
        _context.Participaciones.Add(partCrear);
        _context.SaveChanges();
        return RedirectToAction("Bodas", "Boda");              
    }

    [HttpGet("elimina/participacion/{ParticipacionId}")]
    public IActionResult EliminaParticipacion(int participacionId){
        Participacion? partBorrar = _context.Participaciones.SingleOrDefault(p => p.ParticipacionId == participacionId);
        if(partBorrar != null){
            _context.Participaciones.Remove(partBorrar);
            _context.SaveChanges();
            return RedirectToAction("Bodas", "Boda");
        }
        return View("Bodas");
    }
}