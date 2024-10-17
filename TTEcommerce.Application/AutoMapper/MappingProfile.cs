using AutoMapper;
using TTEcommerce.Application.Dtos;
using TTEcommerce.Domain.ProductAggregate;

namespace TTEcommerce.Application.AutoMapper
 {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}