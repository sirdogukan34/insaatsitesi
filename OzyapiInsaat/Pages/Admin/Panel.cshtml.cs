using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OzyapiInsaat.Data;
using OzyapiInsaat.Models;
using System.Security.Claims;

namespace OzyapiInsaat.Pages.Admin
{
    [Authorize]
    public class PanelModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PanelModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public SiteSettings SiteSettings { get; set; } = new();

        [BindProperty]
        public AboutUs AboutInfo { get; set; } = new();

        [BindProperty]
        public ContactInfo CurrentContact { get; set; } = new();

        [BindProperty]
        public Service NewService { get; set; } = new();

        [BindProperty]
        public Project NewProject { get; set; } = new();

        public List<Service> Services { get; set; } = new();
        public List<Project> Projects { get; set; } = new();

        public async Task OnGetAsync()
        {
            SiteSettings = await _context.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings();
            AboutInfo = await _context.AboutUs.FirstOrDefaultAsync() ?? new AboutUs();
            CurrentContact = await _context.ContactInfo.FirstOrDefaultAsync() ?? new ContactInfo();
            Services = await _context.Services.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostSiteSettings(IFormFile? logoFile, IFormFile? faviconFile)
        {
            var settings = await _context.SiteSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new SiteSettings();
                _context.SiteSettings.Add(settings);
            }

            if (logoFile != null)
            {
                settings.LogoUrl = await SaveFileAsync(logoFile);
            }

            if (faviconFile != null)
            {
                settings.FaviconUrl = await SaveFileAsync(faviconFile);
            }

            settings.FooterCopyright = SiteSettings.FooterCopyright;
            settings.FooterTagline = SiteSettings.FooterTagline;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateContact()
        {
            var contact = await _context.ContactInfo.FirstOrDefaultAsync();
            if (contact == null)
            {
                contact = new ContactInfo();
                _context.ContactInfo.Add(contact);
            }

            contact.Address = CurrentContact.Address;
            contact.Phone = CurrentContact.Phone;
            contact.Email = CurrentContact.Email;
            contact.WorkingHours = CurrentContact.WorkingHours;
            contact.WhatsAppNumber = CurrentContact.WhatsAppNumber;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddService()
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(NewService);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddProject(IFormFile projectImage)
        {
            if (ModelState.IsValid && projectImage != null)
            {
                NewProject.ImageUrl = await SaveFileAsync(projectImage);
                _context.Projects.Add(NewProject);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                // Optionally delete the file from wwwroot/uploads
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAboutUs(IFormFile? aboutImage)
        {
            var about = await _context.AboutUs.FirstOrDefaultAsync();
            if (about == null)
            {
                about = new AboutUs();
                _context.AboutUs.Add(about);
            }

            about.Title = AboutInfo.Title;
            about.Content = AboutInfo.Content;

            if (aboutImage != null)
            {
                about.ImageUrl = await SaveFileAsync(aboutImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/" + uniqueFileName;
        }
    }
}
