using System.ComponentModel.DataAnnotations;

namespace PokedexApi.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        [StringLength(6, MinimumLength = 4)]

        [Required(ErrorMessage = "This field is required")]
        public string PokemonNo { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "This field is required")]
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
