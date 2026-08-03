using Microsoft.AspNetCore.Http.HttpResults;

namespace ffw.Pages.FeatureArtikel;

public static class ArtikelEndpoints
{
    public static IEndpointRouteBuilder MapArtikelEndpoints(this IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/api/artikel")
        .WithTags("Artikel");

        group.MapGet("{id:int}", GetArtikelById)
            .Produces<ArtikelItem>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName(nameof(GetArtikelById))
            .WithSummary("Get Artikel by Id")
            .WithDescription("Get Artikel data");

        return group;

    }

    private static async Task<Results<Ok<ArtikelItem>, NotFound>> GetArtikelById(int id, IArtikelRepository repo)
    {
        var item = await repo.GetByIdAsync(id);
        return item is not null
            ? TypedResults.Ok(item)
            : TypedResults.NotFound();
    }
}
