using PokedexApi.DTO;
using PokedexApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexApi.Repositories
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetAllPokemon();
        Pokemon GetPokemonById(int id);
        Pokemon GetPokemonByPokemonNo(string pokemonNo);
        Task AddPokemon(PokemonDto pokemonDto);
        Task UpdatePokemon(int pokemonId, PokemonDto pokemonDto);
        Task DeletePokemon(int pokemonId);
    }
}
