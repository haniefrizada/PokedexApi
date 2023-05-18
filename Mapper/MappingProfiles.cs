using AutoMapper;
using PokedexApi.DTO;
using PokedexApi.Models;

namespace PokedexApi.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap();
            CreateMap<ApplicationUser, SignUpDTO>().ReverseMap()
            .ForMember(f => f.UserName, t2 => t2.MapFrom(src => src.Email));
        }
    }
}
