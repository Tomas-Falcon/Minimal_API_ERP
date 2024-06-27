using AutoMapper;
using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Api;

internal static class RaincheckApi
{
    public static RouteGroupBuilder MapRaincheckApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Raincheck Api");

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        // Get all rainchecks
        group.MapGet("/rainchecks", async Task<Results<Ok<IList<RaincheckDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            var rainchecks = await db.Rainchecks
                .Include(s => s.Product)
                .Include(s => s.Store)
                .ToListAsync();
            return rainchecks.Any() ? TypedResults.Ok(mapper.Map<IList<RaincheckDto>>(rainchecks)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Get rainchecks with pagination
        group.MapGet("/rainchecksb", async Task<Results<Ok<IList<RaincheckDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            var rainchecks = await db.Rainchecks
                .OrderBy(s => s.RaincheckId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(s => s.Product)
                .Include(s => s.Product.Category)
                .Include(s => s.Store)
                .ToListAsync();
            return rainchecks.Any() ? TypedResults.Ok(mapper.Map<IList<RaincheckDto>>(rainchecks)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Get raincheck by ID
        group.MapGet("/raincheck/{RaincheckIdGuid}", async Task<Results<Ok<RaincheckDto>, NotFound>> (Guid RaincheckIdGuid, AppDbContext db, IMapper mapper) =>
        {
            var raincheck = await db.Rainchecks
                .Include(s => s.Product)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.RaincheckIdGuid == RaincheckIdGuid);
            return raincheck != null ? TypedResults.Ok(mapper.Map<RaincheckDto>(raincheck)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Create a new raincheck
        group.MapPost("/raincheck", async Task<Results<Created<RaincheckDto>, BadRequest>> (RaincheckDto raincheckDto, AppDbContext db, IMapper mapper) =>
        {
            var raincheck = mapper.Map<Raincheck>(raincheckDto);
            db.Rainchecks.Add(raincheck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/raincheck/{raincheck.RaincheckIdGuid}", mapper.Map<RaincheckDto>(raincheck));
        })
        .WithOpenApi();

        // Update an existing raincheck
        group.MapPut("/raincheck/{RaincheckIdGuid}", async Task<Results<Ok<RaincheckDto>, NotFound, BadRequest>> (Guid RaincheckIdGuid, RaincheckDto raincheckDto, AppDbContext db, IMapper mapper) =>
        {
            var raincheck = await db.Rainchecks.FirstOrDefaultAsync(m => m.RaincheckIdGuid == RaincheckIdGuid);
            if (raincheck == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(raincheckDto, raincheck);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<RaincheckDto>(raincheck));
        })
        .WithOpenApi();

        // Delete a raincheck
        group.MapDelete("/raincheck/{RaincheckIdGuid}", async Task<Results<NoContent, NotFound>> (Guid RaincheckIdGuid, AppDbContext db) =>
        {
            var raincheck = await db.Rainchecks.FirstOrDefaultAsync(m => m.RaincheckIdGuid == RaincheckIdGuid);
            if (raincheck == null)
            {
                return TypedResults.NotFound();
            }

            db.Rainchecks.Remove(raincheck);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        return group;
    }
}
