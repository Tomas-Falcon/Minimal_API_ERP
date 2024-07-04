using AutoMapper;
using ERP;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Infraestructure.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<StoreDto, Store>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Store, StoreDtoView>()
                .ForMember(dest => dest.GuidId, opt => opt.MapFrom(src => src.StoreIdGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Product, ProductDtoView>()
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dest => dest.LeadTime, opt => opt.MapFrom(src => src.LeadTime))
                .ForMember(dest => dest.ProductIdGuid, opt => opt.MapFrom(src => src.ProductIdGuid))
                .ForMember(dest => dest.CategoryIdGuid, opt => opt.MapFrom(src => src.Category.CategoryIdGuid));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dest => dest.LeadTime, opt => opt.MapFrom(src => src.LeadTime));

            CreateMap<Raincheck, RaincheckDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderIdGuid, opt => opt.MapFrom(src => src.OrderIdGuid))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<OrderDtoView, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderIdGuid, opt => opt.MapFrom(src => src.OrderIdGuid))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<Category, CategoryDto>()
               .ForMember(dest => dest.CategoryIdGuid, opt => opt.MapFrom(src => src.CategoryIdGuid))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<CategoryDtoView, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.OrderDetailIdGuid, opt => opt.MapFrom(src => src.OrderDetailIdGuid))
              .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
              .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

            CreateMap<OrderDetailDtoView, OrderDetail>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));


            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.CartItemIdGuid, opt => opt.MapFrom(src => src.CartItemIdGuid))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

            CreateMap<CartItemDtoView, CartItem>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());
        }
    }
}
