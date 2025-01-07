using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets for your models
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Drug> Drugs { get; set; }
    public DbSet<DosageSchedule> DosageSchedules { get; set; }
    public DbSet<MedicationEvent> MedicationEvents { get; set; }
    public DbSet<DoctorPatient> DoctorPatients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.Property(e => e.UserRole).HasConversion<string>();

            entity.Property(e => e.InsertedAt)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.LastUpdatedAt)
                  .ValueGeneratedOnAddOrUpdate()
                  .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.Email)
                  .HasMaxLength(256)
                  .IsRequired();

            entity.Property(e => e.Username)
                  .HasMaxLength(128)
                  .IsRequired();

            // Indexes
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();

            // Relationships
            entity.HasOne(u => u.PatientProfile)
                  .WithOne(p => p.User)
                  .HasForeignKey<Patient>(p => p.UserId);

            entity.HasOne(u => u.DoctorProfile)
                  .WithOne(d => d.User)
                  .HasForeignKey<Doctor>(d => d.UserId);
        });

        // Patient entity configuration
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patients");

            entity.Property(e => e.AMKA)
                  .HasMaxLength(15)
                  .IsRequired();

            entity.Property(e => e.DateOfBirth)
                  .IsRequired();

            // Indexes
            entity.HasIndex(e => e.AMKA).IsUnique();

            // Relationships
            entity.HasOne(p => p.User)
                  .WithOne(u => u.PatientProfile)
                  .HasForeignKey<Patient>(p => p.UserId);

            entity.HasMany(p => p.Drugs)
                  .WithOne(d => d.Patient)
                  .HasForeignKey(d => d.PatientId);

            entity.HasMany(p => p.DoctorPatients)
                  .WithOne(dp => dp.Patient)
                  .HasForeignKey(dp => dp.PatientId);
        });

        // Doctor entity configuration
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctors");

            entity.Property(e => e.Specialty)
                  .HasMaxLength(128);

            // Relationships
            entity.HasOne(d => d.User)
                  .WithOne(u => u.DoctorProfile)
                  .HasForeignKey<Doctor>(d => d.UserId);

            entity.HasMany(d => d.DoctorPatients)
                  .WithOne(dp => dp.Doctor)
                  .HasForeignKey(dp => dp.DoctorId);
        });

        // DoctorPatient entity configuration
        modelBuilder.Entity<DoctorPatient>(entity =>
        {
            entity.ToTable("DoctorPatients");

            // Composite key
            entity.HasKey(dp => new { dp.DoctorId, dp.PatientId });

            // Foreign key constraints with NO ACTION
            entity.HasOne(dp => dp.Doctor)
                  .WithMany(d => d.DoctorPatients)
                  .HasForeignKey(dp => dp.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(dp => dp.Patient)
                  .WithMany(p => p.DoctorPatients)
                  .HasForeignKey(dp => dp.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Drug entity configuration
        modelBuilder.Entity<Drug>(entity =>
        {
            entity.ToTable("Drugs");

            entity.Property(e => e.Name)
                  .HasMaxLength(256)
                  .IsRequired();

            entity.Property(e => e.PackageSize)
                  .IsRequired();

            entity.Property(e => e.DoseQuantity)
                  .HasColumnType("decimal(18, 2)");

            // Indexes
            entity.HasIndex(d => d.Name);

            // Relationships
            entity.HasOne(d => d.Patient)
                  .WithMany(p => p.Drugs)
                  .HasForeignKey(d => d.PatientId);
        });

        // DosageSchedule entity configuration
        modelBuilder.Entity<DosageSchedule>(entity =>
        {
            entity.ToTable("DosageSchedules");

            entity.Property(e => e.StartDate)
                  .IsRequired();

            entity.Property(e => e.IntervalDays)
                  .IsRequired();

            entity.Property(e => e.DoseTime)
                  .IsRequired();

            entity.Property(e => e.EndDate);

            // Indexes
            entity.HasIndex(ds => ds.StartDate);

            // Relationships
            entity.HasOne(ds => ds.Drug)
                  .WithMany(d => d.DosageSchedules)
                  .HasForeignKey(ds => ds.DrugId);
        });

        // MedicationEvent entity configuration
        modelBuilder.Entity<MedicationEvent>(entity =>
        {
            entity.ToTable("MedicationEvents");

            entity.Property(e => e.EventDateTime)
                  .IsRequired();

            entity.Property(e => e.IsTaken)
                  .HasDefaultValue(false);

            entity.Property(e => e.TakenAt);

            // Indexes
            entity.HasIndex(me => me.EventDateTime);

            // Relationships
            entity.HasOne(me => me.DosageSchedule)
                  .WithMany(ds => ds.MedicationEvents)
                  .HasForeignKey(me => me.DosageScheduleId);
        });


        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.ToTable("Calendars");

            entity.Property(c => c.Name)
                  .HasMaxLength(128)
                  .IsRequired();

            entity.Property(c => c.Description)
                  .HasMaxLength(512);

            // Relationships
            entity.HasOne(c => c.Patient)
                  .WithMany(p => p.Calendars)
                  .HasForeignKey(c => c.PatientId);
        });

        // CalendarEvent Configuration
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.ToTable("CalendarEvents");

            entity.Property(e => e.Title)
                  .HasMaxLength(128)
                  .IsRequired();

            entity.Property(e => e.Notes)
                  .HasMaxLength(512);

            // Relationships
            entity.HasOne(e => e.Calendar)
                  .WithMany(c => c.Events)
                  .HasForeignKey(e => e.CalendarId);

            entity.HasOne(e => e.MedicationEvent)
                  .WithMany()
                  .HasForeignKey(e => e.MedicationEventId)
                  .OnDelete(DeleteBehavior.Restrict); // Avoid cascade issues
        });
    }
}
