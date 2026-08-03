// ffw.Data removed: data layer is not present
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;


public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi();
        builder.Services.AddRazorPages(o =>
        {
            o.Conventions.AddPageRoute("/PageLink", "/technik-und-wissen");
            o.Conventions.AddPageRoute("/PageLink", "/mannschaft");
            o.Conventions.AddPageRoute("/PageLink", "/fahrzeuge"); 
            // Ensure /artikel resolves to the ArtikelPage in Pages/FeatureArtikel
            o.Conventions.AddPageRoute("/FeatureArtikel/ArtikelPage", "/artikel");
        });
        // Register Artikel services/repository for dependency injection
        builder.Services.AddTransient<ffw.Pages.FeatureArtikel.IArtikelRepository, ffw.Pages.FeatureArtikel.ArtikelRepository>();
        builder.Services.AddTransient<ffw.Pages.FeatureArtikel.IArtikelService, ffw.Pages.FeatureArtikel.ArtikelService>();
        // Data/DbContext was removed. Skip DbContext registration and related services.
        builder.Services.AddValidation();
        // Add in-memory caching for the app
        builder.Services.AddMemoryCache();

        var app = builder.Build();
        // API endpoints for articles/einsaetze were removed with the data layer
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseStatusCodePagesWithReExecute("/PageNotFound");
        app.UseRouting();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();
        app.MapOpenApi();
        app.MapScalarApiReference("/scalar", opt => { opt.Theme = ScalarTheme.Default; });
        app.Run();
    }
}