using System.ComponentModel.DataAnnotations;

namespace QueVistoHoje.API.Repositories.Auth {
    public class LoginRepository {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
