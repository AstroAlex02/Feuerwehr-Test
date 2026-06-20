using ffw.Data;

namespace ffw.Pages.FeatureEinsatz;

public interface IEinsatzService
{
    Task<Einsatz?> GetArtikelByIdAsync(int id);
}

public class EinsatzService(ffwDb db) : IEinsatzService
{
    public Task<Einsatz?> GetArtikelByIdAsync(int id)
    {
        return db.Einsatz.FindAsync(id).AsTask();
    }
}