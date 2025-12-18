using Microsoft.EntityFrameworkCore;
using OzyapiInsaat.Models;

namespace OzyapiInsaat.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Varsayılan verileri ekle
            modelBuilder.Entity<SiteSettings>().HasData(new SiteSettings { Id = 1, FooterCopyright = "© 2024 Özyapı İnşaat. Tüm Hakları Saklıdır.", FooterTagline = "Güvenle Geleceği İnşa Ederiz." });
            modelBuilder.Entity<ContactInfo>().HasData(new ContactInfo { Id = 1, Address = "Örnek Mah. İnşaat Sk. No:123, İstanbul", Phone = "+90 555 123 45 67", Email = "info@ozyapiinsaat.com", WorkingHours = "Pzt - Cmt: 08:00 - 18:00" });
            modelBuilder.Entity<AboutUs>().HasData(new AboutUs { Id = 1, Title = "Neden Özyapı İnşaat?", Content = "Yılların getirdiği tecrübe ile inşaat sektöründe güvenilir ve yenilikçi çözümler sunuyoruz...", ImageUrl = "/uploads/default-about.jpg" });
        }
    }
}

