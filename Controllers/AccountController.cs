using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokedexApi.DTO;
using PokedexApi.Models;
using PokedexApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PokedexApi.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _appConfig;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountRepository accRepo,
                                 IMapper mapper,
                                 IConfiguration appConfig,
                                 UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _repo = accRepo;
            _mapper = mapper;
            _appConfig = appConfig;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Register(SignUpDTO userDTO)
        {
            var user = _mapper.Map<ApplicationUser>(userDTO);

            if (ModelState.IsValid)
            {
                var emailExists = await _repo.IsEmailTakenAsync(userDTO.Email);
                if (emailExists)
                {
                    ModelState.AddModelError("EmailExists", "Email is already taken.");
                    return BadRequest(ModelState);
                }

                var result = await _repo.SignUpUserAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    AddErrorsToModelState(result.Errors);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var issuer = _appConfig["JWT:Issuer"];
            var audience = _appConfig["JWT:Audience"];
            var key = _appConfig["JWT:Key"];

            if (ModelState.IsValid)
            {
                var loginResult = await _repo.SignInUserAsync(loginDTO);

                if (loginResult.Succeeded)
                {
                    var user = await _repo.FindUserByEmailAsync(loginDTO.UserName);

                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id) // Set the user ID as NameIdentifier claim
                        };

                        var keyBytes = Encoding.UTF8.GetBytes(key);
                        var theKey = new SymmetricSecurityKey(keyBytes); // 256 bits of key
                        var creds = new SigningCredentials(theKey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(issuer, audience, null, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), userId = user.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid email or password.");
                }
            }

            return BadRequest(ModelState);
        }

        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
