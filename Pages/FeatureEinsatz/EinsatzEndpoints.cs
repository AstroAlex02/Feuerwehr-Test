using ffw.Data;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ffw.Pages.FeatureEinsatz;

public static class EinsatzEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/api/einsaetze")
        .WithTags("einsaetze");

        group.MapGet("{id:int}", GetCustomerById)
            .Produces<Artikel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName(nameof(GetCustomerById))
            .WithSummary("Get einsaetze by Id")
            .WithDescription("Get einsaetze data");

        return group;

    }

    private static async Task<Results<Ok<Einsatz>, NotFound>> GetCustomerById(int id, IEinsatzService svc)
    {
        var customer = await svc.GetArtikelByIdAsync(id);
        return customer is not null
            ? TypedResults.Ok(customer)
            : TypedResults.NotFound();
    }
}
