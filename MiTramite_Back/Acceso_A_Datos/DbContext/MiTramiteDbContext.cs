using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTramite_Domain.Entities;
using MiTramite_Domain.Constants;

namespace MiTramite_Back.Acceso_A_Datos.Context
{
    public class MiTramiteDbContext : DbContext
    {
        public MiTramiteDbContext(DbContextOptions<MiTramiteDbContext> options) : base(options)
        {
        }

        public DbSet<Archivo> Archivos => Set<Archivo>();
        public DbSet<ArchivosRequeridosTramite> ArchivosRequeridosTramites => Set<ArchivosRequeridosTramite>();
        public DbSet<EstadoTramite> EstadoTramites => Set<EstadoTramite>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Incumplimiento> Incumplimientos => Set<Incumplimiento>();
        public DbSet<Opcion> Opciones => Set<Opcion>();
        public DbSet<Permiso> Permisos => Set<Permiso>();
        public DbSet<Rentista> Rentistas => Set<Rentista>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<RolOpcion> RolOpciones => Set<RolOpcion>();
        public DbSet<RolPermiso> RolPermisos => Set<RolPermiso>();
        public DbSet<SolicitudTramite> SolicitudTramites => Set<SolicitudTramite>();
        public DbSet<TipoArchivo> TipoArchivos => Set<TipoArchivo>();
        public DbSet<TipoTramite> TipoTramites => Set<TipoTramite>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Configuracion de claves primarias compuestas y unicas, indices y filtros globales
            modelBuilder.Entity<ArchivosRequeridosTramite>()
                .HasKey(a => new { a.IdTipoTramite, a.IdTipoArchivo });

            modelBuilder.Entity<RolOpcion>()
                .HasKey(ro => new { ro.IdRol, ro.IdOpcion });

            modelBuilder.Entity<Incumplimiento>()
                .HasKey(i => new { i.IdSolicitudTramite, i.IdFuncionario });

            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.IdRol, rp.IdPermiso });

            modelBuilder.Entity<Funcionario>()
                .HasIndex(f => f.CodigoFuncionario).IsUnique();

            modelBuilder.Entity<Rentista>()
                .HasIndex(r => r.CI).IsUnique();
            #endregion

            #region Relaciones

            modelBuilder.Entity<Archivo>()
                .HasOne(a => a.TipoArchivo)
                .WithMany()
                .HasForeignKey(a => a.IdTipoArchivo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Archivo>()
                .HasOne(a => a.Rentista)
                .WithMany()
                .HasForeignKey(a => a.IdRentista)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArchivosRequeridosTramite>()
                .HasOne(ar => ar.TipoTramite)
                .WithMany()
                .HasForeignKey(ar => ar.IdTipoTramite)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArchivosRequeridosTramite>()
                .HasOne(ar => ar.TipoArchivo)
                .WithMany()
                .HasForeignKey(ar => ar.IdTipoArchivo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Rol)
                .WithMany(r => r.Funcionarios)
                .HasForeignKey(f => f.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Funcionario>()
                .HasMany(f => f.SolicitudesAsignadas)
                .WithOne(st => st.Funcionario)
                .HasForeignKey(st => st.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Funcionario>()
                .HasMany(f => f.Incumplimientos)
                .WithOne(i => i.Funcionario)
                .HasForeignKey(i => i.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incumplimiento>()
                .HasOne(i => i.SolicitudTramite)
                .WithMany()
                .HasForeignKey(i => i.IdSolicitudTramite)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incumplimiento>()
               .HasOne(i => i.FuncionarioReasignado)
               .WithMany()
               .HasForeignKey(i => i.IdFuncionarioReasignado)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolOpcion>()
                .HasOne(o => o.Opcion)
                .WithMany()
                .HasForeignKey(o => o.IdOpcion)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolOpcion>()
                .HasOne(o => o.Rol)
                .WithMany()
                .HasForeignKey(o => o.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany()
                .HasForeignKey(rp => rp.IdRol)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany()
                .HasForeignKey(rp => rp.IdPermiso)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SolicitudTramite>()
                .HasOne(st => st.Rentista)
                .WithMany()
                .HasForeignKey(st => st.IdRentista)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudTramite>()
                .HasOne(st => st.TipoTramite)
                .WithMany()
                .HasForeignKey(st => st.IdTipoTramite)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudTramite>()
                .HasOne(st => st.EstadoTramite)
                .WithMany()
                .HasForeignKey(st => st.IdEstadoTramite)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}