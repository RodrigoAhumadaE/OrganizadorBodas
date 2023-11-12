#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace OrganizadorBodas.Models;

public class Boda{
    [Key]
    public int BodaId {get;set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    public string Novio {get; set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    public string Novia {get; set;}

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    [FechaPasada]
    public DateTime Fecha {get; set;}

    public string fechaComparar {
        get{ return Fecha.ToShortDateString();}
    } 

    [Required(ErrorMessage = "Debe ingresar este dato.")]
    public string Direccion {get; set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
    public DateTime FechaActualizacion {get; set;} = DateTime.Now;

    public int UsuarioId {get; set;}

    public Usuario? Creador {get;set;}

    public List<Participacion> ListaParticipaciones {get;set;} = new List<Participacion>();
}

public class FechaPasadaAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DateTime NowTime = DateTime.Now;
        
        if ((DateTime?)value < NowTime)
        {
            return new ValidationResult("La fecha no puede ser antes de la fecha de hoy");
        } else {
            return ValidationResult.Success;
        }
    }
}