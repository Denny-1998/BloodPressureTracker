using Microsoft.EntityFrameworkCore;

namespace MeasurementService.Models
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Measurements)
                .HasForeignKey(m => m.PatientSSN);
        }
    }
}
