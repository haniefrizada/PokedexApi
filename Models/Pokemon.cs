using System.ComponentModel.DataAnnotations;

namespace PokedexApi.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        [StringLength(6, MinimumLength = 4)]
        [Required(ErrorMessage = "Pokemon number is required.")]
        [RegularExpression("^#.*", ErrorMessage = "Pokemon number must start with '#' symbol.")]
        public string PokemonNo { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Type cannot contain numbers.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public Pokemon()
        {
        }

        public Pokemon(int id, string pokemonNo, string name, string type, string description)
        {
            Id = id;
            PokemonNo = pokemonNo;
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
