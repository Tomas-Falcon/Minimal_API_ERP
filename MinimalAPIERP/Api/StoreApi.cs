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

internal static class StoreApi
{
    public static RouteGroupBuilder MapStoreApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Store Api");

        group.MapGet("/user", (ClaimsPrincipal user) =>
        {
            return user.Identity;
        })
        .WithOpenApi();

        // Get store by ID
        group.MapGet("/store/{StoreIdGuid}", async Task<Results<Ok<StoreDtoView>, NotFound>> (Guid StoreIdGuid, AppDbContext db, IMapper mapper) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.StoreIdGuid == StoreIdGuid);
            return store != null ? TypedResults.Ok(mapper.Map<StoreDtoView>(store)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Get all stores
        group.MapGet("/storea", async Task<Results<Ok<IList<StoreDtoView>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            var stores = await db.Stores.ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreDtoView>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Get stores with pagination
        group.MapGet("/storeb", async Task<Results<Ok<IList<StoreDtoView>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            var stores = await db.Stores.Skip(page * pageSize).Take(pageSize).ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreDtoView>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        // Create a new store
        group.MapPost("/store", async Task<Results<Created<StoreDtoView>, BadRequest>> (StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            var store = mapper.Map<Store>(storeDto);
            db.Stores.Add(store);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/store/{store.StoreIdGuid}", mapper.Map<StoreDtoView>(store));
        })
         .WithOpenApi();

        // Update an existing store
        group.MapPut("/store/{StoreIdGuid}", async Task<Results<Ok<StoreDtoView>, NotFound, BadRequest>> (Guid StoreIdGuid, StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.StoreIdGuid == StoreIdGuid);
            if (store == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(storeDto, store);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<StoreDtoView>(store));
        })
        .WithOpenApi();

        // Delete a store
        group.MapDelete("/store/{StoreIdGuid}", async Task<Results<NoContent, NotFound>> (Guid StoreIdGuid, AppDbContext db) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.StoreIdGuid == StoreIdGuid);
            if (store == null)
            {
                return TypedResults.NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        // JSON options
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        group.MapGet("/storee", async (AppDbContext db) =>
        {
            var stores = await db.Stores.Include(s => s.Rainchecks).ToListAsync();
            return stores.Any() ? Results.Json(stores, options) : Results.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/storef", async (AppDbContext db) =>
        {
            var stores = await db.Stores.Include(s => s.Rainchecks).ToListAsync();
            return stores.Any() ? Results.Json(stores, options) : Results.NotFound();
        })
        .WithOpenApi();

       

        return group;
    }
}
