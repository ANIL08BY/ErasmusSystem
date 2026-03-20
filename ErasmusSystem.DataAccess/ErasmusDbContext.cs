using Microsoft.EntityFrameworkCore;
using ErasmusSystem.Entities;

namespace ErasmusSystem.DataAccess
{
    public class ErasmusDbContext : DbContext
    {
        // Program.cs üzerinden bağlantı ayarlarını (Connection Strings) almak için Constructor
        public ErasmusDbContext(DbContextOptions<ErasmusDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablolara karşılık gelen DbSet'ler
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tablo isimleri eşleştirmesi
            modelBuilder.Entity<User>().ToTable("users", "belek_erasmus");
            modelBuilder.Entity<Application>().ToTable("applications", "belek_erasmus");

            // --- USER (KULLANICI) TABLOSU KOLON EŞLEŞTİRMELERİ ---
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnName("last_name");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<User>().Property(u => u.Role).HasColumnName("role");

            // User sınıfı eşleştirme
            modelBuilder.Entity<User>().Property(u => u.Gno).HasColumnName("gno");
            modelBuilder.Entity<User>().Property(u => u.IsErasmusBefore).HasColumnName("is_erasmus_before");

            // --- APPLICATION (BAŞVURU) TABLOSU KOLON EŞLEŞTİRMELERİ ---
            modelBuilder.Entity<Application>().Property(a => a.Id).HasColumnName("id");
            modelBuilder.Entity<Application>().Property(a => a.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Application>().Property(a => a.Term).HasColumnName("term");
            modelBuilder.Entity<Application>().Property(a => a.Status).HasColumnName("status");
            modelBuilder.Entity<Application>().Property(a => a.TotalScore).HasColumnName("total_score");
            modelBuilder.Entity<Application>().Property(a => a.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<Application>().Property(a => a.UpdatedAt).HasColumnName("updated_at");

            // Email alanı benzersiz (Unique)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}