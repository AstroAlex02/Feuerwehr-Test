using ffw.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ffw.Pages.FeatureArtikel;

public static class EinsatzEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/api/artikel")
        .WithTags("Artikel");

        group.MapGet("{id:int}", GetCustomerById)
            .Produces<Artikel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName(nameof(GetCustomerById))
            .WithSummary("Get Artikel by Id")
            .WithDescription("Get Artikel data");

        return group;

    }

    private static async Task<Results<Ok<Artikel>, NotFound>> GetCustomerById(int id, IArtikelService svc)
    {
        var customer = await svc.GetArtikelByIdAsync(id);
        return customer is not null
            ? TypedResults.Ok(customer)
            : TypedResults.NotFound();
    }
}
