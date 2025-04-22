using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CeilUfas.Data;
using CeilUfas.models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CeilUfas.Pages.Admin
{
    public class AppsettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AppsettingsModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public AppSettings AppSettings { get; set; } = default!;

        [BindProperty]
        public IFormFile LogoFile { get; set; }
        
        public SelectList SessionList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var appsettings = await _context.AppSettings
                .Include(a => a.CurrentSession)
                .FirstOrDefaultAsync();
                
            if (appsettings == null)
            {
                appsettings = new AppSettings();
            }
            
            AppSettings = appsettings;
            
            // Populate sessions dropdown
            SessionList = new SelectList(await _context.Sessions.ToListAsync(), 
                "Id", "Name", AppSettings.CurrentSessionId);
                
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Repopulate sessions dropdown on validation error
                SessionList = new SelectList(await _context.Sessions.ToListAsync(), 
                    "Id", "Name", AppSettings.CurrentSessionId);
                return Page();
            }

            // Handle logo file upload
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + LogoFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(fileStream);
                }

                AppSettings.LogoImagePath = "/uploads/" + uniqueFileName;
            }

            _context.Attach(AppSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Globals.AppSettings = AppSettings;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppSettingsExists(AppSettings.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Appsettings");
        }

        private bool AppSettingsExists(int id)
        {
            return _context.AppSettings.Any(e => e.Id == id);
        }
    }
}
