using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CeilUfas.Data;
using CeilUfas.models;

namespace CeilUfas.Pages.Admin
{
    public class AppsettingsModel : PageModel
    {
        private readonly CeilUfas.Data.ApplicationDbContext _context;

        public AppsettingsModel(CeilUfas.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppSettings AppSettings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
           

            var appsettings =  await _context.AppSettings.FirstOrDefaultAsync();
            if (appsettings == null)
            {
                appsettings = new AppSettings();
            }
            AppSettings = appsettings;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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

            return RedirectToPage("./Index");
        }

        private bool AppSettingsExists(int id)
        {
            return _context.AppSettings.Any(e => e.Id == id);
        }
    }
}
