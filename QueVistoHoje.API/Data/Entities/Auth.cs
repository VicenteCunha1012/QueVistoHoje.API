using System.ComponentModel.DataAnnotations;

namespace QueVistoHoje.API.Data.Entities {
    public class Auth {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
