using Microsoft.EntityFrameworkCore;
using NjalaHopitalAdmittedPatientRecords.Models;

public class NjalaHopitalContext : DbContext
{
    public NjalaHopitalContext(DbContextOptions<NjalaHopitalContext> options) : base(options)
    {
    }

    public DbSet<AdmittedPatientRecord> AdmittedPatientRecords { get; set; }
    public DbSet<Registration> Register { get; set; } // Add this line
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>().HasKey(u => u.Id);

        modelBuilder.Entity<Registration>().HasIndex(u => u.UserName).
            IsUnique(); // optional but good



        base.OnModelCreating(modelBuilder);
    }
}
