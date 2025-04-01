using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GreenServices.Models;

public partial class PlantContext : DbContext
{
    public PlantContext()
    {
    }

    public PlantContext(DbContextOptions<PlantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plantconfiguration> Plantconfigurations { get; set; }

    public virtual DbSet<Sensorreading> Sensorreadings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=GreenphytoDB;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plantconfiguration>(entity =>
        {
            entity.HasKey(e => e.TrayId).HasName("plantconfiguration_pkey");

            entity.ToTable("plantconfiguration");

            entity.Property(e => e.TrayId)
                .ValueGeneratedNever()
                .HasColumnName("tray_id");
            entity.Property(e => e.PlantType).HasColumnName("plant_type");
            entity.Property(e => e.TargetHumidity).HasColumnName("target_humidity");
            entity.Property(e => e.TargetLight).HasColumnName("target_light");
            entity.Property(e => e.TargetTemperature).HasColumnName("target_temperature");
        });

        modelBuilder.Entity<Sensorreading>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sensorreading_pkey");

            entity.ToTable("sensorreading");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.Light).HasColumnName("light");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.TrayId).HasColumnName("tray_id");

            entity.HasOne(d => d.Tray).WithMany(p => p.Sensorreadings)
                .HasForeignKey(d => d.TrayId)
                .HasConstraintName("sensorreading_tray_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
