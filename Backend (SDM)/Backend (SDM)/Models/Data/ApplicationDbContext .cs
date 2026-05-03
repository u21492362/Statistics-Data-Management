using Backend__SDM_.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Backend__SDM_.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<District> Districts => Set<District>();
        public DbSet<Circuit> Circuits => Set<Circuit>();
        public DbSet<Society> Societies => Set<Society>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<StatisticalYear> StatisticalYears => Set<StatisticalYear>();
        public DbSet<StatisticalCategory> StatisticalCategories => Set<StatisticalCategory>();
        public DbSet<StatisticalRegister> StatisticalRegisters => Set<StatisticalRegister>();
        public DbSet<RegisterMemberEntry> RegisterMemberEntries => Set<RegisterMemberEntry>();
        public DbSet<RegisterMemberCategory> RegisterMemberCategories => Set<RegisterMemberCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureDistrict(modelBuilder);
            ConfigureCircuit(modelBuilder);
            ConfigureSociety(modelBuilder);
            ConfigureAppUser(modelBuilder);
            ConfigureMember(modelBuilder);
            ConfigureStatisticalYear(modelBuilder);
            ConfigureStatisticalCategory(modelBuilder);
            ConfigureStatisticalRegister(modelBuilder);
            ConfigureRegisterMemberEntry(modelBuilder);
            ConfigureRegisterMemberCategory(modelBuilder);
        }

        private static void ConfigureDistrict(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Code)
                    .HasMaxLength(50);

                entity.HasIndex(x => x.Name).IsUnique();
            });
        }

        private static void ConfigureCircuit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Circuit>(entity =>
            {
                entity.ToTable("Circuit");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Code)
                    .HasMaxLength(50);

                entity.HasIndex(x => new { x.DistrictId, x.Name }).IsUnique();

                entity.HasOne(x => x.District)
                    .WithMany(x => x.Circuits)
                    .HasForeignKey(x => x.DistrictId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureSociety(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Society>(entity =>
            {
                entity.ToTable("Society");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Code)
                    .HasMaxLength(50);

                entity.HasIndex(x => new { x.CircuitId, x.Name }).IsUnique();

                entity.HasOne(x => x.Circuit)
                    .WithMany(x => x.Societies)
                    .HasForeignKey(x => x.CircuitId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureAppUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.PasswordHash)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(x => x.Role)
                    .IsRequired();

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                entity.Property(x => x.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(x => x.CreatedOnUtc)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(x => x.ModifiedOnUtc)
                    .IsRequired(false);

                entity.HasIndex(x => x.Email)
                    .IsUnique();

                entity.HasOne(x => x.Circuit)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.CircuitId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(x => x.Society)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.SocietyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });
        }

        private static void ConfigureMember(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.MembershipNumber)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.FullName)
                    .HasMaxLength(250)
                    .IsRequired();

                entity.Property(x => x.MobileNumber)
                    .HasMaxLength(20);

                entity.Property(x => x.PhysicalAddress)
                    .HasMaxLength(250);

                entity.HasIndex(x => x.MembershipNumber).IsUnique();
                entity.HasIndex(x => new { x.SocietyId, x.FullName });

                entity.HasOne(x => x.Society)
                    .WithMany(x => x.Members)
                    .HasForeignKey(x => x.SocietyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureStatisticalYear(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatisticalYear>(entity =>
            {
                entity.ToTable("StatisticalYear");

                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.Year).IsUnique();
            });
        }

        private static void ConfigureStatisticalCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatisticalCategory>(entity =>
            {
                entity.ToTable("StatisticalCategory");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Code)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.Description)
                    .HasMaxLength(500);

                entity.HasIndex(x => x.Code).IsUnique();
                entity.HasIndex(x => x.DisplayOrder);
            });
        }

        private static void ConfigureStatisticalRegister(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatisticalRegister>(entity =>
            {
                entity.ToTable("StatisticalRegister");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Notes)
                    .HasMaxLength(1000);

                entity.HasIndex(x => new { x.StatisticalYearId, x.SocietyId }).IsUnique();

                entity.HasOne(x => x.StatisticalYear)
                    .WithMany(x => x.StatisticalRegisters)
                    .HasForeignKey(x => x.StatisticalYearId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.District)
                    .WithMany(x => x.StatisticalRegisters)
                    .HasForeignKey(x => x.DistrictId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Circuit)
                    .WithMany(x => x.StatisticalRegisters)
                    .HasForeignKey(x => x.CircuitId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Society)
                    .WithMany(x => x.StatisticalRegisters)
                    .HasForeignKey(x => x.SocietyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.CompiledByUser)
                    .WithMany()
                    .HasForeignKey(x => x.CompiledByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureRegisterMemberEntry(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterMemberEntry>(entity =>
            {
                entity.ToTable("RegisterMemberEntry");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Remarks)
                    .HasMaxLength(500);

                entity.HasIndex(x => new { x.StatisticalRegisterId, x.MemberId }).IsUnique();
                entity.HasIndex(x => new { x.StatisticalRegisterId, x.RowNumber }).IsUnique();

                entity.HasOne(x => x.StatisticalRegister)
                    .WithMany(x => x.RegisterMemberEntries)
                    .HasForeignKey(x => x.StatisticalRegisterId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Member)
                    .WithMany(x => x.RegisterEntries)
                    .HasForeignKey(x => x.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureRegisterMemberCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterMemberCategory>(entity =>
            {
                entity.ToTable("RegisterMemberCategory");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.ValueText)
                    .HasMaxLength(250);

                entity.HasIndex(x => new { x.RegisterMemberEntryId, x.StatisticalCategoryId }).IsUnique();

                entity.HasOne(x => x.RegisterMemberEntry)
                    .WithMany(x => x.Categories)
                    .HasForeignKey(x => x.RegisterMemberEntryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.StatisticalCategory)
                    .WithMany(x => x.RegisterMemberCategories)
                    .HasForeignKey(x => x.StatisticalCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
