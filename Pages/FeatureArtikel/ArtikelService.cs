using ffw.Data;

namespace ffw.Pages.FeatureArtikel;

public interface IArtikelService
{
    Task<Artikel?> GetArtikelByIdAsync(int id);
}

public class EinsatzService(ffwDb db) : IArtikelService
{
    public Task<Artikel?> GetArtikelByIdAsync(int id)
    {
        return db.Artikel.FindAsync(id).AsTask();
    }
}