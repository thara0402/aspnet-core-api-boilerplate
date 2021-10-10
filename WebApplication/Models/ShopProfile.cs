using AutoMapper;
using WebApplication.Infrastructure.Sql.Models;

namespace WebApplication.Models
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
