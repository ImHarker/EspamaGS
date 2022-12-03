using Microsoft.AspNetCore.Identity;

namespace EspamaGS_2._0.Data {
    public static class InitRoles {
        public static void Init(RoleManager<IdentityRole> roleManager) {
            if (roleManager.Roles.Any()) return;
            roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            roleManager.CreateAsync(new IdentityRole("Funcionario")).Wait();
            roleManager.CreateAsync(new IdentityRole("Cliente")).Wait();
        }
    }
}
