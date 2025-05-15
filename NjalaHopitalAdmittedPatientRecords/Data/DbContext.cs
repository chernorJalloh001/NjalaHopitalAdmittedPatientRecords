using Microsoft.EntityFrameworkCore;
using NjalaHopitalAdmittedPatientRecords.Models;

public class NjalaHopitalContext : DbContext
{
    public NjalaHopitalContext(DbContextOptions<NjalaHopitalContext> options) : base(options)
    {
    }

    public DbSet<AdmittedPatientRecord> AdmittedPatientRecords { get; set; }
    public DbSet<Login> Login { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure primary keys
        modelBuilder.Entity<AdmittedPatientRecord>()
            .HasKey(a => a.ID);

        modelBuilder.Entity<Login>()
            .HasKey(l => l.UserName);

        base.OnModelCreating(modelBuilder);
    }
}
