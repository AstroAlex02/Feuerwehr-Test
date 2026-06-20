using ffw.Data;
using ffw.Pages.FeatureArtikel;
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
        });
        builder.Services.AddDbContext<ffwDb>(options => options.UseSqlServer());
        builder.Services.AddValidation();
        builder.Services.AddScoped<IArtikelService, EinsatzService>();
        // Add in-memory caching for the app
        builder.Services.AddMemoryCache();

        var app = builder.Build();
        app.MapCustomerEndpoints();
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