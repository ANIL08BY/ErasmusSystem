using Microsoft.EntityFrameworkCore;
using ErasmusSystem.Entities;

namespace ErasmusSystem.DataAccess
{
    public class ErasmusDbContext : DbContext
    {
        // Program.cs üzerinden bağlantı ayarlarını (Connection String) almak için Constructor
        public ErasmusDbContext(DbContextOptions<ErasmusDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablolara karşılık gelen DbSet'ler
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Veritabanını tasarlayan arkadaşınızın 6. hafta raporunda belirlediği şema ve tablo isimleriyle eşleştirme
            modelBuilder.Entity<User>().ToTable("users", "belek_erasmus");
            modelBuilder.Entity<Application>().ToTable("applications", "belek_erasmus");

            // Email alanının benzersiz (Unique) olması kuralı
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}