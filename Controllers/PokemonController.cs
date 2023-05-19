using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using PokedexApi.Models;
using PokedexApi.Data;
using Microsoft.EntityFrameworkCore;
using PokedexApi.DTO;
using PokedexApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace PokedexApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonController(PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPokemon()
        {
            var result = await _pokemonRepository.GetAllPokemon();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetPokemonById(int id)
        {
            var result = _pokemonRepository.GetPokemonById(id);

            if (result == null)
            {
                var errormessage = $"Pokemon ID {id} not found.";
                return NotFound(errormessage);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPokemon(PokemonDto pokemonDto)
        {
            await _pokemonRepository.AddPokemon(pokemonDto);

            /*var success = $"Successfully added new Pokemon";*/
            return Ok(pokemonDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, PokemonDto pokemonDto)
        {
            await _pokemonRepository.UpdatePokemon(id, pokemonDto);

            /*var success = $"Successfully updated PokemonId {id}";*/
            return Ok(pokemonDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            await _pokemonRepository.DeletePokemon(id);
            return NoContent();
        }
    }
}
