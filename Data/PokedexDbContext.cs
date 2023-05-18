using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokedexApi.Models;

namespace PokedexApi.Data
{
    public class PokedexDbContext : IdentityDbContext<ApplicationUser>
    {
        public PokedexDbContext(DbContextOptions<PokedexDbContext> options) : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons => Set<Pokemon>();
    }
}
