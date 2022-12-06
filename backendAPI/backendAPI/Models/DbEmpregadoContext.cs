using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backendAPI.Models;

public partial class DbEmpregadoContext : DbContext
{
    public DbEmpregadoContext()
    {
    }

    public DbEmpregadoContext(DbContextOptions<DbEmpregadoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empregado> Empregados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__C225F98D0F9FD2BF");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.DataFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empregado>(entity =>
        {
            entity.HasKey(e => e.IdEmpregado).HasName("PK__Empregad__CD7F15668E0512AE");

            entity.ToTable("Empregado");

            entity.Property(e => e.IdEmpregado).HasColumnName("idEmpregado");
            entity.Property(e => e.Contrato).HasColumnType("datetime");
            entity.Property(e => e.DataFecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empregados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Empregado__idDep__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
