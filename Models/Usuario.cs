#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OrganizadorBodas.Models;

public class Usuario{
    [Key]
    public int UsuarioId {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    public string Nombre {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    public string Apellido {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
    [EmailUnico]
    public string Email {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    [MinLength(8, ErrorMessage = "El Password debe tener al menos 8 caracteres.")]
    public string Password {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    [NotMapped]
    [Compare("Password", ErrorMessage = "El password no coincide.")]
    public string PassConfirm {get; set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get; set;} = DateTime.Now;

    public List<Boda> ListaBodas {get; set;} = new List<Boda>();

    public List<Participacion> ListaParticipaciones {get;set;} = new List<Participacion>();
}

public class EmailUnicoAttribute : ValidationAttribute{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
        if(value == null){
            return new ValidationResult("Email is required!");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

    	if(_context.Usuarios.Any(e => e.Email == value.ToString())){
            return new ValidationResult("El Email ya está registrado.");
        } else {
            return ValidationResult.Success;
        }
    }
}