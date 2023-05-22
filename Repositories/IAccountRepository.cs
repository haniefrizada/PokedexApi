using Microsoft.AspNetCore.Identity;
using PokedexApi.DTO;
using PokedexApi.Models;
using System.Threading.Tasks;

namespace PokedexApi.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpUserAsync(ApplicationUser user, string password);
        Task<SignInResult> SignInUserAsync(LoginDTO loginDTO);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
