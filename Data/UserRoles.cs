using HotelListing.Data;
using Microsoft.AspNetCore.Identity;

namespace Demo_Project.Data;

public class UserRoles
{
    private UserManager<ApiUser> userManager;
    private RoleManager<IdentityRole> roleManager;
    public string Role { get; set; }

    public UserRoles(UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager; 
    }

    public async Task ProcessAsync()
    {
        List<string> names = new List<string>();
        IdentityRole role = await roleManager.FindByIdAsync(Role);
        if (role != null)
        {
            foreach (var user in userManager.Users)
            {
                if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    names.Add(user.UserName);
            }
        }
    }

}
