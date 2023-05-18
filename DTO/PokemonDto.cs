using System.ComponentModel.DataAnnotations;

namespace PokedexApi.DTO
{
    public class PokemonDto
    {
        [StringLength(6, MinimumLength = 4)]
        public string PokemonNo { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public PokemonDto()
        {
        }

        public PokemonDto(string pokemonNo, string name, string type, string description)
        {
            PokemonNo = pokemonNo;
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
