using HotelListing.Data;
using Microsoft.AspNetCore.Identity;

namespace Demo_Project.Data;

public class RoleEdit
{
    public IdentityRole Role { get; set; }
    public IEnumerable<ApiUser> Members { get; set; }
    public IEnumerable<ApiUser> NonMembers { get; set; }
}