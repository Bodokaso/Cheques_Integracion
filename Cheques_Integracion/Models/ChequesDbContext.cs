using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cheques_Integracion.Models;

public partial class ChequesDbContext : DbContext
{
    public ChequesDbContext()
    {
    }

    public ChequesDbContext(DbContextOptions<ChequesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConceptosPago> ConceptosPagos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<RegistroSolicitudCheque> RegistroSolicitudCheques { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); DataBase=cheques_db; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConceptosPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__concepto__3213E83F7B31307F");

            entity.ToTable("conceptos_pago");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proveedo__3213E83F2A2A61A3");

            entity.ToTable("proveedores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CuentaContableProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cuenta_contable_proveedor");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_identificacion");
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_persona");
        });

        modelBuilder.Entity<RegistroSolicitudCheque>(entity =>
        {
            entity.HasKey(e => e.NumeroSolicitud).HasName("PK__registro__33FCA19947596112");

            entity.ToTable("registro_solicitud_cheque");

            entity.Property(e => e.NumeroSolicitud)
                .ValueGeneratedNever()
                .HasColumnName("numero_solicitud");
            entity.Property(e => e.CuentaContableProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cuenta_contable_proveedor");
            entity.Property(e => e.CuentaContableRelacionada)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cuenta_contable_relacionada");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("date")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Proveedor).HasColumnName("proveedor");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.RegistroSolicitudCheques)
                .HasForeignKey(d => d.Proveedor)
                .HasConstraintName("FK__registro___prove__2A4B4B5E");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9754D45B14");

            entity.ToTable("usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
