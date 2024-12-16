using Microsoft.AspNetCore.Identity;

namespace QueVistoHoje.API.Data {
    public class ApplicationUser : IdentityUser {
        public string? Nome { get; set; }
        public string? NIF { get; set; }
        public string? Morada { get; set; }
        public string? Imagem { get; set; }

    }

}
