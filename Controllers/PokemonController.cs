using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using PokedexApi.Models;
using PokedexApi.Data;
using Microsoft.EntityFrameworkCore;
using PokedexApi.DTO;
using PokedexApi.Repositories;

namespace PokedexApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonController(PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet("Pokemons")]
        public async Task<IActionResult> GetAllPokemon()
        {
            var result = await _pokemonRepository.GetAllPokemon();
            return Ok(result);
        }

        [HttpGet("GetPokemon/{id}")]
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

        [HttpGet("PokemonNo/{pokemonNo}")]
        public IActionResult GetPokemonByPokemonNo(string pokemonNo)
        {
            var result = _pokemonRepository.GetPokemonByPokemonNo(pokemonNo);

            if (result == null)
            {
                var errormessage = $"Pokemon No {pokemonNo} not found.";
                return NotFound(errormessage);
            }

            return Ok(result);
        }

        [HttpPost("AddPokemon")]
        public async Task<IActionResult> AddPokemon(PokemonDto pokemonDto)
        {
            await _pokemonRepository.AddPokemon(pokemonDto);

            var success = $"Successfully added new Pokemon";
            return Ok(success);
        }

        [HttpPut("UpdatePokemon/{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, PokemonDto pokemonDto)
        {
            await _pokemonRepository.UpdatePokemon(id, pokemonDto);

            var success = $"Successfully updated PokemonId {id}";
            return Ok(success);
        }

        [HttpDelete("DeletePokemon/{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            await _pokemonRepository.DeletePokemon(id);
            return NoContent();
        }
    }
}
