using GestionTriage.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTriage.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Paciente?> Pacientes { get; set; }
    public DbSet<NivelTriage> NivelesTriage { get; set; }
    public DbSet<RegistroTriage> RegistrosTriage { get; set; }
    public DbSet<ReglaAsignacion> ReglasAsignacion { get; set; }
    public DbSet<Hospital> Hospitales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones de las relaciones y restricciones

        modelBuilder.Entity<RegistroTriage>()
            .HasOne(rt => rt.Paciente)
            .WithMany(p => p.RegistrosTriage)
            .HasForeignKey(rt => rt.PacienteId);

        modelBuilder.Entity<RegistroTriage>()
            .HasOne(rt => rt.NivelTriage)
            .WithMany(nt => nt.RegistrosTriage)
            .HasForeignKey(rt => rt.NivelTriageId);

        modelBuilder.Entity<RegistroTriage>()
            .HasOne(rt => rt.Hospital)
            .WithMany(h => h.RegistrosTriage)
            .HasForeignKey(rt => rt.HospitalId);

        modelBuilder.Entity<ReglaAsignacion>()
            .HasOne(ra => ra.NivelTriage)
            .WithMany(nt => nt.ReglasAsignacion)
            .HasForeignKey(ra => ra.NivelTriageId);
    }
}