using System.Threading.Tasks;

namespace ffw.Pages.FeatureArtikel;

public interface IArtikelService
{
    Task<ArtikelItem?> GetArtikelByIdAsync(int id);
}

public class ArtikelService : IArtikelService
{
    private readonly IArtikelRepository _repo;
    public ArtikelService(IArtikelRepository repo)
    {
        _repo = repo;
    }

    public Task<ArtikelItem?> GetArtikelByIdAsync(int id) => _repo.GetByIdAsync(id);
}
