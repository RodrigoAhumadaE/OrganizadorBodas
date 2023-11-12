﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrganizadorBodas.Models;

#nullable disable

namespace OrganizadorBodas.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrganizadorBodas.Models.Boda", b =>
                {
                    b.Property<int>("BodaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Novia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Novio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("BodaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Bodas");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Participacion", b =>
                {
                    b.Property<int>("ParticipacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BodaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ParticipacionId");

                    b.HasIndex("BodaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Participaciones");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Boda", b =>
                {
                    b.HasOne("OrganizadorBodas.Models.Usuario", "Creador")
                        .WithMany("ListaBodas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Participacion", b =>
                {
                    b.HasOne("OrganizadorBodas.Models.Boda", "Boda")
                        .WithMany("ListaParticipaciones")
                        .HasForeignKey("BodaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrganizadorBodas.Models.Usuario", "Usuario")
                        .WithMany("ListaParticipaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boda");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Boda", b =>
                {
                    b.Navigation("ListaParticipaciones");
                });

            modelBuilder.Entity("OrganizadorBodas.Models.Usuario", b =>
                {
                    b.Navigation("ListaBodas");

                    b.Navigation("ListaParticipaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
