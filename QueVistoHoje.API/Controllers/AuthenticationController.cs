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

        private string GenerateJwtToken(IdentityUser user) {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Get JWT settings from configuration and convert the key to bytes
            var key = GenerateRandomKey(32);  // Example length (256-bit key)

            // Ensure the key is at least 128 bits (16 bytes)
            if (key.Length < 16) {
                throw new ArgumentException("The key for JWT is too short. It must be at least 128 bits (16 bytes).");
            }

            var tokenDescriptor = new SecurityTokenDescriptor {
                // Subject holds the claims about the user
                Subject = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        }),

                // Expiry time of the token
                Expires = DateTime.UtcNow.AddHours(1),

                // Additional claims like issuer and audience
                Issuer = configuration["JWT:Issuer"],    // Optional but good for added security
                Audience = configuration["JWT:Audience"],

                // Signing credentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            // Generate the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Convert the token to a string representation
            return tokenHandler.WriteToken(token);
        }


        private byte[] GenerateRandomKey(int lengthInBytes) {
            var randomBytes = new byte[lengthInBytes];

            // Use RandomNumberGenerator to fill the byte array with secure random values
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomBytes); // Generate secure random bytes
            }

            return randomBytes;
        }

    }
}
