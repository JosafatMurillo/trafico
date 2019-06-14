using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServicioTransito.Models
{
    public partial class TransitoContext : DbContext
    {
        public TransitoContext()
        {
        }

        public TransitoContext(DbContextOptions<TransitoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalogo> Catalogo { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Dictamen> Dictamen { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<Reporte> Reporte { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<VehiculoAgeno> VehiculoAgeno { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Transito;UID=transito;PWD=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => e.IdCatalogo);

                entity.Property(e => e.IdCatalogo).HasColumnName("idCatalogo");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCatalogo).HasColumnName("tipoCatalogo");

                entity.HasOne(d => d.TipoCatalogoNavigation)
                    .WithMany(p => p.InverseTipoCatalogoNavigation)
                    .HasForeignKey(d => d.TipoCatalogo)
                    .HasConstraintName("FK_Catalogo_Catalogo");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Apellidos)
                    .HasColumnName("apellidos")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasenia)
                    .HasColumnName("contrasenia")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroLicencia)
                    .HasColumnName("numeroLicencia")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dictamen>(entity =>
            {
                entity.HasKey(e => e.IdDictamen);

                entity.Property(e => e.IdDictamen).HasColumnName("idDictamen");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasColumnType("text");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fechaHora")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Folio)
                    .HasColumnName("folio")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePerito)
                    .HasColumnName("nombrePerito")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Personal).HasColumnName("personal");

                entity.Property(e => e.Reporte).HasColumnName("reporte");

                entity.HasOne(d => d.PersonalNavigation)
                    .WithMany(p => p.Dictamen)
                    .HasForeignKey(d => d.Personal)
                    .HasConstraintName("FK_Dictamen_Personal");

                entity.HasOne(d => d.ReporteNavigation)
                    .WithMany(p => p.Dictamen)
                    .HasForeignKey(d => d.Reporte)
                    .HasConstraintName("FK_Dictamen_Reporte");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasKey(e => e.IdFoto);

                entity.Property(e => e.IdFoto).HasColumnName("idFoto");

                entity.Property(e => e.Foto1)
                    .HasColumnName("foto")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Reporte).HasColumnName("reporte");

                entity.HasOne(d => d.ReporteNavigation)
                    .WithMany(p => p.Foto)
                    .HasForeignKey(d => d.Reporte)
                    .HasConstraintName("FK_Foto_Reporte");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => e.IdPersonal);

                entity.Property(e => e.IdPersonal).HasColumnName("idPersonal");

                entity.Property(e => e.Apellidos)
                    .HasColumnName("apellidos")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Catalogo).HasColumnName("catalogo");

                entity.Property(e => e.Contrasenia)
                    .HasColumnName("contrasenia")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasColumnName("nombreUsuario")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CatalogoNavigation)
                    .WithMany(p => p.Personal)
                    .HasForeignKey(d => d.Catalogo)
                    .HasConstraintName("FK_Personal_Catalogo");
            });

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.HasKey(e => e.IdReporte);

                entity.Property(e => e.IdReporte).HasColumnName("idReporte");

                entity.Property(e => e.Cliente).HasColumnName("cliente");

                entity.Property(e => e.Lugar)
                    .HasColumnName("lugar")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreConductorAgeno)
                    .HasColumnName("nombreConductorAgeno")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Vehiculo).HasColumnName("vehiculo");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_Reporte_Cliente");

                entity.HasOne(d => d.VehiculoNavigation)
                    .WithMany(p => p.Reporte)
                    .HasForeignKey(d => d.Vehiculo)
                    .HasConstraintName("FK_Reporte_Reporte");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");

                entity.Property(e => e.Anio)
                    .HasColumnName("anio")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cliente).HasColumnName("cliente");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasColumnName("marca")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasColumnName("modelo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreAseguradora)
                    .HasColumnName("nombreAseguradora")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPlacas)
                    .HasColumnName("numeroPlacas")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasColumnName("numeroPoliza")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_Vehiculo_Vehiculo");
            });

            modelBuilder.Entity<VehiculoAgeno>(entity =>
            {
                entity.HasKey(e => e.IdVehiculoAgeno);

                entity.Property(e => e.IdVehiculoAgeno).HasColumnName("idVehiculoAgeno");

                entity.Property(e => e.Anio)
                    .HasColumnName("anio")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasColumnName("marca")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasColumnName("modelo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreAseguradora)
                    .HasColumnName("nombreAseguradora")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPlacas)
                    .HasColumnName("numeroPlacas")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasColumnName("numeroPoliza")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Reporte).HasColumnName("reporte");

                entity.HasOne(d => d.ReporteNavigation)
                    .WithMany(p => p.VehiculoAgeno)
                    .HasForeignKey(d => d.Reporte)
                    .HasConstraintName("FK_VehiculoAgeno_Reporte");
            });
        }
    }
}
