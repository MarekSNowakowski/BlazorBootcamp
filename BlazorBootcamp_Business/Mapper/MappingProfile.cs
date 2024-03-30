using AutoMapper;
using BlazorBootcamp_DataAccess;
using BlazorBootcamp_Models;

namespace BlazorBootcamp_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
