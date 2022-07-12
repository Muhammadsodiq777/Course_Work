using System.ComponentModel.DataAnnotations;

namespace Demo_Project.Data;

public class RoleModifcation
{
    [Required]
    public string RoleName { get; set; }
    public string UserName { get; set; }
}
