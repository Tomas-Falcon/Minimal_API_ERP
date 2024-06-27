using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;
using AutoMapper;
using System.Text.Json;
using System.Text.Json.Serialization;
using ERP;

namespace MinimalAPIERP.Api
{
    public static class CategoryApi
    {
        public static RouteGroupBuilder MapCategoryApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/erp")
                .WithTags("Category Api");

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            // Get all categories
            group.MapGet("/categories", async Task<Results<Ok<IList<CategoryDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            {
                var categories = await db.Categories.ToListAsync();
                return categories.Any() ? TypedResults.Ok(mapper.Map<IList<CategoryDto>>(categories)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Get category by ID
            group.MapGet("/category/{CategoryIdGuid}", async Task<Results<Ok<CategoryDto>, NotFound>> (Guid CategoryIdGuid, AppDbContext db, IMapper mapper) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(m => m.CategoryIdGuid == CategoryIdGuid);
                return category != null ? TypedResults.Ok(mapper.Map<CategoryDto>(category)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Create a new category
            group.MapPost("/category", async Task<Results<Created<CategoryDto>, BadRequest>> (CategoryDtoView categoryDto, AppDbContext db, IMapper mapper) =>
            {
                var category = mapper.Map<Category>(categoryDto);
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/erp/category/{category.CategoryIdGuid}", mapper.Map<CategoryDto>(category));
            })
            .WithOpenApi();

            // Update an existing category
            group.MapPut("/category/{CategoryIdGuid}", async Task<Results<Ok<CategoryDto>, NotFound, BadRequest>> (Guid CategoryIdGuid, CategoryDtoView categoryDto, AppDbContext db, IMapper mapper) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(m => m.CategoryIdGuid == CategoryIdGuid);
                if (category == null)
                {
                    return TypedResults.NotFound();
                }

                mapper.Map(categoryDto, category);
                await db.SaveChangesAsync();
                return TypedResults.Ok(mapper.Map<CategoryDto>(category));
            })
            .WithOpenApi();

            // Delete a category
            group.MapDelete("/category/{CategoryIdGuid}", async Task<Results<NoContent, NotFound>> (Guid CategoryIdGuid, AppDbContext db) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(m => m.CategoryIdGuid == CategoryIdGuid);
                if (category == null)
                {
                    return TypedResults.NotFound();
                }

                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithOpenApi();

            return group;
        }
    }
}
