using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QueVistoHoje.API.Repositories.Auth;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              IConfiguration configuration) {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRepository model) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = new IdentityUser {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Registado com sucesso!" });

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRepository model) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new { Message = "Credenciais inválidas!" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new { Message = "Credenciais inválidas!" });

            var token = GenerateJwtToken(user);

            return Ok(new {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            });

        }

        // POST: api/auth/refresh-token
        [HttpPost("refresh-token")]
        [Authorize]
        public IActionResult RefreshToken() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null) return Unauthorized();

            var newToken = GenerateJwtToken(user);

            return Ok(new {
                Token = newToken,
                Expiration = DateTime.UtcNow.AddHours(1)
            });
        }

        private string GenerateJwtToken(IdentityUser user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
