using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CeilUfas.Data;
using CeilUfas.models;
using CeilUfas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add this line to enable role management
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add this section to ensure AppSettings exists
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    if (!context.AppSettings.Any())
    {
        context.AppSettings.Add(new AppSettings
        {
            OrganizationName = "Default Organization",
            Address = "123 Main St",
            Tel = "123-456-7890",
            Email = "info@organization.com",
            Website = "https://organization.com",
            Facebook = "https://facebook.com/organization",
            Twitter = "https://twitter.com/organization",
            LinkedIn = "https://linkedin.com/company/organization",
            YouTube = "https://youtube.com/organization",
            RegistrationIsOpened = false
        });
        await context.SaveChangesAsync();

    }

        CeilUfas.Globals.AppSettings = context.AppSettings.FirstOrDefault();


}

// Add this section to ensure roles exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    foreach (var roleName in CeilUfas.Globals.RoleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
