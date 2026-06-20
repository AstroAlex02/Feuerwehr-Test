using ffw.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ffw.Pages.FeatureArtikel;


public class ArtikelPage(ffwDb db, IArtikelService artikelService, IMemoryCache cache) : MasterPage
{
    public List<Artikel> ArtikelListe = new();
    public Artikel? Artikel;


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var timeout = TimeSpan.FromSeconds(120);

        if (id != null)
        {
            Artikel = await cache.GetOrCreateAsync($"Artikel_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = timeout;
                return await artikelService.GetArtikelByIdAsync((int)id);
            });

            if (Artikel != null)
            {
                NewTile = Artikel.Ueberschrift + " - ";
            }
            else
            {
                Response.StatusCode = 404;
                NewTile = "404 Seite nicht gefunden - ";
                return new ViewResult { ViewName = "/Pages/PageNotFound.cshtml" };
            }
        }
        else
        {
            ArtikelListe = await cache.GetOrCreateAsync("ArtikelListe_Top", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = timeout;
                // Ensure no tracking so cached entities are detached from DbContext
                return await db.Artikel.OrderByDescending(x => x.Datum).AsNoTracking().Take(30).ToListAsync();
            }) ?? new List<Artikel>();
        }
        return Page();

    }
}
