using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RESTfullAPIRoComputer
{
    public partial class RoComputerContext : DbContext
    {
        public RoComputerContext()
        {
        }

        public RoComputerContext(DbContextOptions<RoComputerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonData> PersonData { get; set; }
        public virtual DbSet<Rotur> Rotur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=lula.database.windows.net;Initial Catalog=3_semester_projekt;Persist Security Info=True;User ID=luca1291;Password=Simpel123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Efternavn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fornavn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonData>(entity =>
            {
                entity.HasIndex(e => e.FkEmail)
                    .HasName("fkIdx_38");

                entity.Property(e => e.Acceleration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FkEmail)
                    .IsRequired()
                    .HasColumnName("FK_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hastighed).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkEmailNavigation)
                    .WithMany(p => p.PersonData)
                    .HasForeignKey(d => d.FkEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_38");
            });

            modelBuilder.Entity<Rotur>(entity =>
            {
                entity.ToTable("rotur");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AntalRoTag).HasColumnName("antal_ro_tag");

                entity.Property(e => e.Tid)
                    .HasColumnName("tid")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}