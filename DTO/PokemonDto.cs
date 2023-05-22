using System.ComponentModel.DataAnnotations;

namespace PokedexApi.DTO
{
    public class PokemonDto
    {
        [StringLength(6, MinimumLength = 4)]
        [Required(ErrorMessage = "Pokemon number is required.")]
        [RegularExpression("^(#\\d+|\\d+)$", ErrorMessage = "Pokemon number should start with '#' symbol and contain only digits or should contain only digits.")]
        public string PokemonNo { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Type cannot contain numbers.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required.")]
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
