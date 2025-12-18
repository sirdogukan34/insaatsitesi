using System.ComponentModel.DataAnnotations;

namespace OzyapiInsaat.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
    }

    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }

    public class SiteSettings
    {
        public int Id { get; set; }
        public string LogoUrl { get; set; } = string.Empty;
        public string FaviconUrl { get; set; } = string.Empty;

        // YENİ EKLENEN ALANLAR
        public string? FooterCopyright { get; set; }
        public string? FooterTagline { get; set; }
    }

    public class AboutUs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class ContactInfo
    {
        public int Id { get; set; }

        [Display(Name = "Adres")]
        public string? Address { get; set; }   // nullable → boş bırakılabilir

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }     // nullable → boş bırakılabilir

        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string? Email { get; set; }     // nullable → boş bırakılabilir

        [Display(Name = "Çalışma Saatleri")]
        public string? WorkingHours { get; set; } // nullable → boş bırakılabilir

        [Display(Name = "WhatsApp")]
        public string? WhatsAppNumber { get; set; } // nullable
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
    }
}
