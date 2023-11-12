﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using OrganizadorBodas.Models;

namespace OrganizadorBodas.Controllers;

public class UsuarioController : Controller{
    private readonly ILogger<UsuarioController> _logger;
    private MyContext _context;

    public UsuarioController(ILogger<UsuarioController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    // GET
    [HttpGet("")]
    public IActionResult Index(){
        return View("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return View("Index");
    }

    // POST
    [HttpPost("procesa/registro")]
    public IActionResult ProcesaRegistro(Usuario usuario){
        if(ModelState.IsValid){
            PasswordHasher<Usuario> Hasher = new PasswordHasher<Usuario>();
            usuario.Password = Hasher.HashPassword(usuario, usuario.Password);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            HttpContext.Session.SetString("email", usuario.Email);
            return RedirectToAction("Bodas", "Boda");
        }
        return View("Index");
    }

    [HttpPost("procesa/login")]
    public IActionResult ProcesaLogin(Login login){
        if(ModelState.IsValid){
            Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Email == login.EmailLogin);
            if(usuario != null){
                Console.WriteLine($"{usuario.Nombre} {usuario.Apellido}");
                PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
                var result = Hasher.VerifyHashedPassword(login, usuario.Password, login.PasswordLogin);
                if(result != 0){
                   HttpContext.Session.SetString("email", usuario.Email);
                   return RedirectToAction("Bodas", "Boda");
                }    
            }
            ModelState.AddModelError("PasswordLogin", "Credenciales incorrectas");
            return View("Index");
        }
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context){
        string? email = context.HttpContext.Session.GetString("email");
        
        if(email == null){
            context.Result = new RedirectToActionResult("Index", "Usuario", null);
        }
    }
}
