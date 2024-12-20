using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QueVistoHoje.API.Data.Entities;
using QueVistoHoje.API.Data;
using System.Security.Cryptography;

namespace QueVistoHoje.API.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthenticationController(
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IConfiguration configuration
            ) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Auth model) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = new ApplicationUser {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, "admin");
            return Ok(new { Message = "Registado com sucesso!" });

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Auth model) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new { Message = "Credenciais inválidas!" });

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new { Message = "Credenciais inválidas!" });

            var token = GenerateJwtToken(user);

            var roles = await userManager.GetRolesAsync(user);
            Console.WriteLine(string.Join(", ", roles));  // Should output "admin"

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

            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null) return Unauthorized();

            var newToken = GenerateJwtToken(user);

            return Ok(new {
                Token = newToken,
                Expiration = DateTime.UtcNow.AddHours(1)
            });
        }

        private string GenerateJwtToken(ApplicationUser user) {
            // Use a 256-bit key for HS256
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            if (key.Length < 32) {
                throw new Exception("JWT key must be at least 256 bits (32 bytes).");
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            var roles = userManager.GetRolesAsync(user).Result;
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expiration = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
