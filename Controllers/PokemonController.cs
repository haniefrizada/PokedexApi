using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using PokedexApi.Models;
using PokedexApi.Data;
using Microsoft.EntityFrameworkCore;
using PokedexApi.DTO;
using PokedexApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet("pokemonNo/{pokemonNo}")]
        public IActionResult GetPokemonByPokemonNo(string pokemonNo)
        {
            var result = _pokemonRepository.GetPokemonByPokemonNo(pokemonNo);

            if (result == null)
            {
                var errormessage = $"Pokemon Number {pokemonNo} not found.";
                return NotFound(errormessage);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPokemon(PokemonDto pokemonDto)
        {
            if (!ModelState.IsValid)
            {
                var errorDetails = new List<string>();
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errorDetails.Add(error.ErrorMessage);
                }
                return BadRequest(new { message = "Validation errors occurred.", errors = errorDetails });
            }

            var existingPokemon = _pokemonRepository.GetPokemonByPokemonNo(pokemonDto.PokemonNo);
            if (existingPokemon != null)
            {
                var errorMessage = $"Pokemon number {pokemonDto.PokemonNo} already exists.";
                return Conflict(new { message = errorMessage });
            }

            await _pokemonRepository.AddPokemon(pokemonDto);

            var successMessage = "New Pokemon added successfully.";
            return Ok(new { message = successMessage, pokemon = pokemonDto });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, PokemonDto pokemonDto)
        {
            if (!ModelState.IsValid)
            {
                var errorDetails = new List<string>();
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errorDetails.Add(error.ErrorMessage);
                }
                return BadRequest(new { message = "Validation errors occurred.", errors = errorDetails });
            }

            var existingPokemon = _pokemonRepository.GetPokemonById(id);
            if (existingPokemon == null)
            {
                var errorMessage = $"Pokemon ID {id} not found.";
                return NotFound(new { message = errorMessage });
            }

            await _pokemonRepository.UpdatePokemon(id, pokemonDto);

            var successMessage = $"Pokemon with ID {id} updated successfully.";
            return Ok(new { message = successMessage, pokemon = pokemonDto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var existingPokemon = _pokemonRepository.GetPokemonById(id);
            if (existingPokemon == null)
            {
                var errorMessage = $"Pokemon ID {id} not found.";
                return NotFound(new { message = errorMessage });
            }

            await _pokemonRepository.DeletePokemon(id);

            var successMessage = $"Pokemon with ID {id} deleted successfully.";
            return Ok(new { message = successMessage });
        }
    }
}
