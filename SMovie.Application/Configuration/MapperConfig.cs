using AutoMapper;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;
using SMovie.Domain.Models.Person;

namespace SMovie.Application.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Mapping User
            CreateMap<User, UserTemporary>().ReverseMap();

            // Mapping Movie
            CreateMap<Movie, MovieSlide>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.MovieCategories));
            CreateMap<MovieCategory, Category>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Category!.Name));

            // Person 
            CreateMap<Person, PersonPreview>();
        }
    }
}
