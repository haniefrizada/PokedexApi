
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PokedexApi.Data;
using PokedexApi.DTO;
using PokedexApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PokedexApi.Repositories
{
    public class PokemonRepository 
    {
        private readonly PokedexDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PokemonRepository(PokedexDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Pokemon>> GetAllPokemon()
        {
            return await _context.Pokemons.FromSqlRaw("EXEC GetAllPokemon").ToListAsync();
        }

        public Pokemon GetPokemonById(int id)
        {
            var result = _context.Pokemons.FromSqlRaw("EXEC GetPokemonById @PokemonId", new SqlParameter("@PokemonId", id))
                .AsEnumerable()                         
                .FirstOrDefault();

            return result;
        }

        public Pokemon GetPokemonByPokemonNo(string pokemonNo)
        {
            /*var result = _context.Pokemons.FirstOrDefault(p => p.PokemonNo == pokemonNo);*/
            var result = _context.Pokemons.FromSqlRaw("EXEC GetPokemonByPokemonNo @PokemonNo", new SqlParameter("@PokemonNo", pokemonNo))
                .AsEnumerable()
                .FirstOrDefault();
            return result;
        }

        public async Task AddPokemon(PokemonDto pokemonDto)
        {
            var existingPokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.PokemonNo == pokemonDto.PokemonNo);
            if (existingPokemon != null)
            {
                throw new ValidationException("Pokemon number already exists.");
            }

            var pokemonNoParam = new SqlParameter("@PokemonNo", pokemonDto.PokemonNo);
            var nameParam = new SqlParameter("@Name", pokemonDto.Name);
            var typeParam = new SqlParameter("@Type", pokemonDto.Type);
            var descParam = new SqlParameter("@Description", pokemonDto.Description);

            await _context.Database.ExecuteSqlRawAsync("EXEC AddPokemon @PokemonNo, @Name, @Type, @Description",
                pokemonNoParam, nameParam, typeParam, descParam);
        }

        public async Task UpdatePokemon(int pokemonId, PokemonDto pokemonDto)
        {
            var pokemonIdParam = new SqlParameter("@PokemonId", pokemonId);
            var pokemonNoParam = new SqlParameter("@PokemonNo", pokemonDto.PokemonNo);
            var nameParam = new SqlParameter("@Name", pokemonDto.Name);
            var typeParam = new SqlParameter("@Type", pokemonDto.Type);
            var descParam = new SqlParameter("@Description", pokemonDto.Description);

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdatePokemon @PokemonId, @PokemonNo, @Name, @Type, @Description",
                pokemonIdParam, pokemonNoParam, nameParam, typeParam, descParam);
        }

        public async Task DeletePokemon(int pokemonId)
        {
            var parameter = new SqlParameter("@PokemonId", pokemonId);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeletePokemon @PokemonId", parameter);
        }
    }
}
