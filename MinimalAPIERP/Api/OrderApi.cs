using ERP.Data;
using ERP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;
using AutoMapper;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinimalAPIERP.Api
{
    public static class OrderApi
    {
        public static RouteGroupBuilder MapOrderApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/erp")
                .WithTags("Order Api");


                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            // Get all orders
            group.MapGet("/orders", async Task<Results<Ok<IList<OrderDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            {
                var orders = await db.Orders.Include(o => o.OrderDetails).ToListAsync();
                return orders.Any() ? TypedResults.Ok(mapper.Map<IList<OrderDto>>(orders)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Get order by ID
            group.MapGet("/order/{OrderIdGuid}", async Task<Results<Ok<OrderDto>, NotFound>> (Guid OrderIdGuid, AppDbContext db, IMapper mapper) =>
            {
                var order = await db.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(m => m.OrderIdGuid == OrderIdGuid);
                return order != null ? TypedResults.Ok(mapper.Map<OrderDto>(order)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Create a new order
            group.MapPost("/order", async Task<Results<Created<OrderDto>, BadRequest>> (OrderDtoView orderDto, AppDbContext db, IMapper mapper) =>
            {
                var order = mapper.Map<Order>(orderDto);
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/erp/order/{order.OrderIdGuid}", mapper.Map<OrderDto>(order));
            })
            .WithOpenApi();

            // Update an existing order
            group.MapPut("/order/{OrderIdGuid}", async Task<Results<Ok<OrderDto>, NotFound, BadRequest>> (Guid OrderIdGuid, OrderDtoView orderDto, AppDbContext db, IMapper mapper) =>
            {
                var order = await db.Orders.FirstOrDefaultAsync(m => m.OrderIdGuid == OrderIdGuid);
                if (order == null)
                {
                    return TypedResults.NotFound();
                }

                mapper.Map(orderDto, order);
                await db.SaveChangesAsync();
                return TypedResults.Ok(mapper.Map<OrderDto>(order));
            })
            .WithOpenApi();

            // Delete an order
            group.MapDelete("/order/{OrderIdGuid}", async Task<Results<NoContent, NotFound>> (Guid OrderIdGuid, AppDbContext db) =>
            {
                var order = await db.Orders.FirstOrDefaultAsync(m => m.OrderIdGuid == OrderIdGuid);
                if (order == null)
                {
                    return TypedResults.NotFound();
                }

                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithOpenApi();

            return group;
        }
    }
}
