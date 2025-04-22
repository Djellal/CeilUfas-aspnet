using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CeilUfas.models;
using CeilUfas.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CeilUfas.Pages.Admin;

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

    public async Task<IActionResult> OnGetAsync()
    {
        AppSettings = await _context.AppSettings.FirstOrDefaultAsync() ?? new AppSettings();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existingSettings = await _context.AppSettings.FirstOrDefaultAsync();
        
        // Handle logo file upload
        if (LogoFile != null && LogoFile.Length > 0)
        {
            // Create uploads directory if it doesn't exist
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
        
            // Generate unique filename
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + LogoFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        
            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await LogoFile.CopyToAsync(fileStream);
            }
        
            // Update the logo path
            AppSettings.LogoImagePath = "/uploads/" + uniqueFileName;
        }
        else if (existingSettings != null && string.IsNullOrEmpty(AppSettings.LogoImagePath))
        {
            // Preserve the existing logo if no new one was uploaded and the form didn't provide a path
            AppSettings.LogoImagePath = existingSettings.LogoImagePath;
        }
    
        if (existingSettings != null)
        {
            // Manually update properties instead of using SetValues to avoid Id modification
            existingSettings.OrganizationName = AppSettings.OrganizationName;
            existingSettings.Address = AppSettings.Address;
            existingSettings.Tel = AppSettings.Tel;
            existingSettings.Email = AppSettings.Email;
            existingSettings.Website = AppSettings.Website;
            existingSettings.Facebook = AppSettings.Facebook;
            existingSettings.Twitter = AppSettings.Twitter;
            existingSettings.LinkedIn = AppSettings.LinkedIn;
            existingSettings.YouTube = AppSettings.YouTube;
            existingSettings.RegistrationIsOpened = AppSettings.RegistrationIsOpened;
            existingSettings.LogoImagePath = AppSettings.LogoImagePath;
        }
        else
        {
            _context.AppSettings.Add(AppSettings);
        }
    
        await _context.SaveChangesAsync();
        Globals.AppSettings = existingSettings ?? AppSettings;
        
        // For debugging
        Console.WriteLine($"Logo path saved: {AppSettings.LogoImagePath}");
        
        return RedirectToPage("./Appsettings");
    }
}