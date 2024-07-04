using ERP;
using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;
using AutoMapper;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinimalAPIERP.Api
{
    public static class CartItemApi
    {
        public static RouteGroupBuilder MapCartItemApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/erp")
                .WithTags("CartItem Api");

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            // Get all cart items
            group.MapGet("/cartitems", async Task<Results<Ok<IList<CartItemDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            {
                var cartItems = await db.CartItems.ToListAsync();
                return cartItems.Any() ? TypedResults.Ok(mapper.Map<IList<CartItemDto>>(cartItems)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Get cart item by ID
            group.MapGet("/cartitem/{CartItemIdGuid}", async Task<Results<Ok<CartItemDto>, NotFound>> (Guid CartItemIdGuid, AppDbContext db, IMapper mapper) =>
            {
                var cartItem = await db.CartItems.FirstOrDefaultAsync(ci => ci.CartItemIdGuid == CartItemIdGuid);
                return cartItem != null ? TypedResults.Ok(mapper.Map<CartItemDto>(cartItem)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Create a new cart item
            group.MapPost("/cartitem", async Task<Results<Created<CartItemDto>, BadRequest>> (CartItemDtoView cartItemDto, AppDbContext db, IMapper mapper) =>
            {
                var cartItem = mapper.Map<CartItem>(cartItemDto);
                db.CartItems.Add(cartItem);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/erp/cartitem/{cartItem.CartItemIdGuid}", mapper.Map<CartItemDto>(cartItem));
            })
            .WithOpenApi();

            // Update an existing cart item
            group.MapPut("/cartitem/{CartItemIdGuid}", async Task<Results<Ok<CartItemDto>, NotFound, BadRequest>> (Guid CartItemIdGuid, CartItemDtoView cartItemDto, AppDbContext db, IMapper mapper) =>
            {
                var cartItem = await db.CartItems.FirstOrDefaultAsync(ci => ci.CartItemIdGuid == CartItemIdGuid);
                if (cartItem == null)
                {
                    return TypedResults.NotFound();
                }

                mapper.Map(cartItemDto, cartItem);
                await db.SaveChangesAsync();
                return TypedResults.Ok(mapper.Map<CartItemDto>(cartItem));
            })
            .WithOpenApi();

            // Delete a cart item
            group.MapDelete("/cartitem/{CartItemIdGuid}", async Task<Results<NoContent, NotFound>> (Guid CartItemIdGuid, AppDbContext db) =>
            {
                var cartItem = await db.CartItems.FirstOrDefaultAsync(ci => ci.CartItemIdGuid == CartItemIdGuid);
                if (cartItem == null)
                {
                    return TypedResults.NotFound();
                }

                db.CartItems.Remove(cartItem);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithOpenApi();

            return group;
        }
    }
}
