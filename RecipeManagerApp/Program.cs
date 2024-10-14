using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using RecipeManagerApp.Models; // Ensure this matches your namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register your DbContext with the SQLite database
builder.Services.AddDbContext<RecipeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RecipeContext")));

// Configure Data Protection for handling encryption in production
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys")) // Change path as needed for key persistence
    .SetApplicationName("RecipeManagerApp");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Custom error page
    app.UseHsts(); // Use HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files (e.g., CSS, JS, images)

app.UseRouting(); // Enable routing for controllers

app.UseAuthorization(); // Enable authorization for protected routes

// Configure endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Optionally add a fallback route or endpoint for specific pages
// app.MapFallbackToPage("/Index");

app.Run(); // Run the application
