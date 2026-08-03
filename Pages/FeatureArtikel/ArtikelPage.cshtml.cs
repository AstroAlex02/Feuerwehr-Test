using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ffw.Pages.FeatureArtikel;

public class ArtikelPage(IArtikelRepository repo, IMemoryCache cache) : ffw.Pages.MasterPage
{
    public List<ArtikelItem> ArtikelListe = new();
    public ArtikelItem? Artikel;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var timeout = TimeSpan.FromSeconds(120);

        if (id != null)
        {
            Artikel = await cache.GetOrCreateAsync($"Artikel_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = timeout;
                return await repo.GetByIdAsync((int)id);
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
                var all = await repo.GetAllAsync();
                return all.OrderByDescending(x => x.Datum).Take(30).ToList();
            }) ?? new List<ArtikelItem>();
        }
        return Page();

    }
}
