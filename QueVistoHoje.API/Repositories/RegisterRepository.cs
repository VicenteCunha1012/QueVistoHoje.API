using System.ComponentModel.DataAnnotations;

namespace QueVistoHoje.API.Repositories {

    public class RegisterRepository {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
