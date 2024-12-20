using Microsoft.AspNetCore.Identity;

namespace QueVistoHoje.API.Data {
    public static class ApplicationDbContextSeed {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager) {
            string[] roleNames = ["Admin", "User", "Manager", "Guest"];

            foreach (var roleName in roleNames) {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
