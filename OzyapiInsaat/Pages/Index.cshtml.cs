using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OzyapiInsaat.Data;
using OzyapiInsaat.Models;

namespace OzyapiInsaat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Sayfada göstermek için verileri tutacak özellikler
        public List<Service> Services { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public AboutUs AboutInfo { get; set; } = new();
        public ContactInfo ContactDetails { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sayfa yüklendiðinde çalýþacak metod
        public async Task OnGetAsync()
        {
            // Veritabanýndan tüm gerekli verileri çekiyoruz
            Services = await _context.Services.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
            AboutInfo = await _context.AboutUs.FirstOrDefaultAsync() ?? new AboutUs();
            ContactDetails = await _context.ContactInfo.FirstOrDefaultAsync() ?? new ContactInfo();
        }
    }
}

