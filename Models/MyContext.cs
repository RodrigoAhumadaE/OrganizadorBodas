#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace OrganizadorBodas.Models;

public class MyContext : DbContext{
    public DbSet<Usuario> Usuarios {get;set;}
    public DbSet<Boda> Bodas {get;set;}
    public DbSet<Participacion> Participaciones {get;set;}

    public MyContext(DbContextOptions options) : base(options){}
}