using Microsoft.AspNetCore.Identity;
using PokedexApi.DTO;
using PokedexApi.Models;

namespace PokedexApi.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpUserAsync(ApplicationUser user, string password);
        Task<SignInResult> SignInUserAsync(LoginDTO loginDTO);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
    }
}
