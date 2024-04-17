using AutoMapper;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;

namespace SMovie.Application.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Mapping User
            CreateMap<User, UserTemporary>().ReverseMap();
            CreateMap<User, UserDetail>().ReverseMap();
            CreateMap<PagedList<User>, PagedList<UserDetail>>();
            CreateMap<User, UserChosen>();

            // Mapping Movie
            CreateMap<Movie, MovieSlide>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.MovieCategories));

            CreateMap<MovieCategory, Category>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Category!.Name));

            CreateMap<Movie, MoviePreview>();
            CreateMap<PagedList<Movie>, PagedList<MoviePreview>>();

            CreateMap<Movie, InfoMovie>()
                .ForMember(dest => dest.FeatureName,
                    opt => opt.MapFrom(src => src.Feature.Name))
                .ForMember(dest => dest.NationName,
                    opt => opt.MapFrom(src => src.Nation.Name))
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories));
            CreateMap<PagedList<Movie>, PagedList<InfoMovie>>();

            CreateMap<Movie, MovieDetail>()
                .ForMember(dest => dest.CastCharacteries,
                opt => opt.MapFrom(src => src.Casts))
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.MovieCategories))
                .ForMember(dest => dest.TotalSeasons,
                opt => opt.MapFrom(src => src.Seasons.Count))
                .ForMember(dest => dest.TotalEpisodes,
                opt => opt.MapFrom(src => src.Seasons.Sum(x => x.Episodes.Count)));

            // Person 
            CreateMap<Person, PersonPreview>();
            CreateMap<PagedList<Person>, PagedList<PersonPreview>>();
            CreateMap<Person, CastCharacter>();
            CreateMap<PagedList<Person>, PagedList<CastCharacter>>();

            CreateMap<Cast, CastCharacter>()
                .ForMember(dest => dest.PersonId,
                opt => opt.MapFrom(src => src.Actor.PersonId))
                .ForMember(dest => dest.NamePerson,
                opt => opt.MapFrom(src => src.Actor.NamePerson))
                .ForMember(dest => dest.Thumbnail,
                opt => opt.MapFrom(src => src.Actor.Thumbnail));

            // Season
            CreateMap<Season, SeasonDTO>();

            // Episode
            CreateMap<Episode, EpisodeDTO>();
        }
    }
}
