using AutoMapper;
using B2BMart.DataLayer.Models;
using B2BMart.Models;
using B2BMart.Models.DTOs;

namespace B2BMart.API.Utility
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, User>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<Product, ProductAddDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.ProductBrandName))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.ProductTypeName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Order, OrdersDTO>()
                .ForMember(d => d.DeliveryMethodId, o => o.MapFrom(s => s.Delivery.ShortName));
                //.ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        }
    }
}
