using Microsoft.EntityFrameworkCore;
using profsysinf.Core.Aggregates;
using profsysinf.Core.Entities;
using projsysinf.Core.Aggregates;

namespace projsysinf.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<DicOperationType> DicOperationTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.IdUser);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).HasMaxLength(255).IsRequired();
                entity.Property(u => u.Password).HasMaxLength(255).IsRequired();
                entity.Property(u => u.IsActive).IsRequired();
            });

            modelBuilder.Entity<DicOperationType>(entity =>
            {
                entity.HasKey(ot => ot.IdOperationType);
                entity.Property(ot => ot.OperationTypeName).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(l => l.IdLog);
                entity.Property(l => l.Tmstmp).IsRequired();

                entity.HasOne(l => l.User)
                      .WithMany(u => u.Logs)
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(l => l.OperationType)
                      .WithMany(ot => ot.Logs)
                      .HasForeignKey(l => l.OperationTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PasswordHistory>(entity =>
            {
                entity.HasKey(ph => ph.IdHistory);
                entity.Property(ph => ph.Password).HasMaxLength(255).IsRequired();
                entity.Property(ph => ph.Tmstmp).IsRequired();

                entity.HasOne(ph => ph.User)
                      .WithMany(u => u.PasswordHistories)
                      .HasForeignKey(ph => ph.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
