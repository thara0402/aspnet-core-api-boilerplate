using AutoMapper;
using System;

namespace WebApplication.Models
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            // SQL
            CreateMap<Infrastructure.Sql.Models.Product, ProductResponse>()
                .ForMember(dst => dst.ProductId, opt => opt.MapFrom(src => src.ProductId.ToString()));
            CreateMap<ProductRequest, Infrastructure.Sql.Models.Product>();

            // Cosmos DB
            CreateMap<Infrastructure.Cosmos.Models.Product, ProductResponse>()
                .ForMember(dst => dst.ProductId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ProductRequest, Infrastructure.Cosmos.Models.Product>();

            // Table Storage
            CreateMap<Infrastructure.Table.Models.Product, ProductResponse>()
                .ForMember(dst => dst.ProductId, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => (decimal)src.Price));
            CreateMap<ProductRequest, Infrastructure.Table.Models.Product>()
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => Decimal.ToDouble(src.Price)));
        }
    }
}
