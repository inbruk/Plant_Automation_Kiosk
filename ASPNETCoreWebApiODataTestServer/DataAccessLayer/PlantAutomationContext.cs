using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASPNETCoreWebApiODataTestServer.DataAccessLayer
{
    public partial class PlantAutomationContext : DbContext
    {
        public PlantAutomationContext()
        {
        }

        public PlantAutomationContext(DbContextOptions<PlantAutomationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrashRequestLog> CrashRequestLog { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<PassLeanLog> PassLeanLog { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Workshop> Workshop { get; set; }
        public virtual DbSet<vwMachinePlace> vwMachinePlace { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=HOME-PC\\SQLSERVER2012;Initial Catalog=PlantAutomation;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrashRequestLog>(entity =>
            {
                entity.Property(e => e.MachineInventoryNumber)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.MachineInventoryNumberNavigation)
                    .WithMany(p => p.CrashRequestLog)
                    .HasForeignKey(d => d.MachineInventoryNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CraRog_MacInvNum");

                entity.HasOne(d => d.NewStatus)
                    .WithMany(p => p.CrashRequestLog)
                    .HasForeignKey(d => d.NewStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CrashRequestLog_StatusId");

                entity.HasOne(d => d.Confirmation)
                    .WithMany(p => p.CrashRequestLog)
                    .HasForeignKey(d => d.ConfirmationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CrashRequestLog_ConfirmationId");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.InventoryNumber);

                entity.Property(e => e.InventoryNumber)
                    .HasMaxLength(32)
                    .ValueGeneratedNever();

                entity.Property(e => e.Model).HasMaxLength(64);

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Machine)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Machine_StatusId");
            });

            modelBuilder.Entity<PassLeanLog>(entity =>
            {
                entity.Property(e => e.PassNumber)
                    .IsRequired()
                    .HasMaxLength(9);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(e => new { e.WorkshopNumber, e.SiteNumber, e.Number });

                entity.Property(e => e.CurrMachineInvNum).HasMaxLength(32);

                entity.HasOne(d => d.CurrMachineInvNumNavigation)
                    .WithMany(p => p.Place)
                    .HasForeignKey(d => d.CurrMachineInvNum)
                    .HasConstraintName("FK_Place_MachineNum");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => new { e.WorkshopNumber, e.Number });

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Site)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Site_StatusId");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.Property(e => e.Number).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Workshop)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workshop_StatusId");
            });

            modelBuilder.Entity<vwMachinePlace>(entity =>
            {
                entity.HasKey(e => new { e.WorkshopNumber, e.SiteNumber, e.Number });

                entity.Property(e => e.CurrMachineInvNum).HasMaxLength(32);

                entity.Property(e => e.CurrMachineInvNum)
                    .HasMaxLength(32)
                    .ValueGeneratedNever();

                entity.Property(e => e.Model).HasMaxLength(64);

                entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}
