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
    public static class OrderDetailsApi
    {
        public static RouteGroupBuilder MapOrderDetailsApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/erp")
                .WithTags("OrderDetails Api");

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            // Get all order details
            group.MapGet("/orderdetails", async Task<Results<Ok<IList<OrderDetailDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            {
                var orderDetails = await db.OrderDetails.ToListAsync();
                return orderDetails.Any() ? TypedResults.Ok(mapper.Map<IList<OrderDetailDto>>(orderDetails)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Get order detail by ID
            group.MapGet("/orderdetail/{OrderDetailIdGuid}", async Task<Results<Ok<OrderDetailDto>, NotFound>> (Guid OrderDetailIdGuid, AppDbContext db, IMapper mapper) =>
            {
                var orderDetail = await db.OrderDetails.FirstOrDefaultAsync(od => od.OrderDetailIdGuid == OrderDetailIdGuid);
                return orderDetail != null ? TypedResults.Ok(mapper.Map<OrderDetailDto>(orderDetail)) : TypedResults.NotFound();
            })
            .WithOpenApi();

            // Create a new order detail
            group.MapPost("/orderdetail", async Task<Results<Created<OrderDetailDto>, BadRequest>> (OrderDetailDtoView orderDetailDto, AppDbContext db, IMapper mapper) =>
            {
                var orderDetail = mapper.Map<OrderDetail>(orderDetailDto);
                db.OrderDetails.Add(orderDetail);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/erp/orderdetail/{orderDetail.OrderDetailIdGuid}", mapper.Map<OrderDetailDto>(orderDetail));
            })
            .WithOpenApi();

            // Update an existing order detail
            group.MapPut("/orderdetail/{OrderDetailIdGuid}", async Task<Results<Ok<OrderDetailDto>, NotFound, BadRequest>> (Guid OrderDetailIdGuid, OrderDetailDtoView orderDetailDto, AppDbContext db, IMapper mapper) =>
            {
                var orderDetail = await db.OrderDetails.FirstOrDefaultAsync(od => od.OrderDetailIdGuid == OrderDetailIdGuid);
                if (orderDetail == null)
                {
                    return TypedResults.NotFound();
                }

                mapper.Map(orderDetailDto, orderDetail);
                await db.SaveChangesAsync();
                return TypedResults.Ok(mapper.Map<OrderDetailDto>(orderDetail));
            })
            .WithOpenApi();

            // Delete an order detail
            group.MapDelete("/orderdetail/{OrderDetailIdGuid}", async Task<Results<NoContent, NotFound>> (Guid OrderDetailIdGuid, AppDbContext db) =>
            {
                var orderDetail = await db.OrderDetails.FirstOrDefaultAsync(od => od.OrderDetailIdGuid == OrderDetailIdGuid);
                if (orderDetail == null)
                {
                    return TypedResults.NotFound();
                }

                db.OrderDetails.Remove(orderDetail);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            })
            .WithOpenApi();

            return group;
        }
    }
}
