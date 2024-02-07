using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTO.Mappings
{
    public class MappingProfile : Profile  // esta herdando de Profile
    {
        // aqui vai ser criado o mapeamento
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
