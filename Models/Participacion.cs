#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace OrganizadorBodas.Models;

public class Participacion{
    [Key]
    public int ParticipacionId {get;set;}

    public int UsuarioId {get;set;}
    public int BodaId {get;set;}

    public Usuario? Usuario {get; set;}
    public Boda? Boda {get;set;}

    public DateTime FechaCreacion {get;set;} = DateTime.Now;
}