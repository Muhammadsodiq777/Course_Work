using Demo_Project.Data;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelListing.IRepository;
using AutoMapper;

namespace Demo_Project.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RoleController> _logger;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public RoleController(RoleManager<IdentityRole> roleManager,
        ILogger<RoleController> logger, UserManager<ApiUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("/create/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateRole([FromBody] string name)
    {
        if (ModelState.IsValid)
        {
            var IsRoleExist = await _roleManager.FindByNameAsync(name);
            if (IsRoleExist == null)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    _logger.LogError($"Role Created named: {name}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogError($"Invalid Role try named: {name}");
                    return BadRequest("Failed");
                }
            }
            else
            {
                _logger.LogError($"Role already exist: {name}");
                return BadRequest($"Role {name} already exist");
            }
        }
        return BadRequest("Invalid ModelState");
    }

    [HttpDelete("/delete/roles")]
    public async Task<IActionResult> DeleteRole([FromBody] string name)
    {
        IdentityRole role = await _roleManager.FindByNameAsync(name);
        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                _logger.LogError($"Role Deleted named: {name}");
                return Ok(result);
            }
            else
            {
                _logger.LogError($"Invalid Role try named: {name}");
                return BadRequest("Failed");
            }
        }
        else
            ModelState.AddModelError("", "No role found");
        return BadRequest("Invalid ModelState");
    }
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] string name)
    {
        IdentityRole role = await _roleManager.FindByNameAsync(name);
        List<ApiUser> members = new List<ApiUser>();
        List<ApiUser> nonMembers = new List<ApiUser>();
        foreach (ApiUser user in _userManager.Users)
        {
            var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
            list.Add(user);
        }
        new RoleEdit
        {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        };

        return Ok(role);
    }

    [HttpPost("/add/user/role")]
    public async Task<IActionResult> GiveUserRole([FromBody] RoleModifcation model)
    {
        IdentityResult result;
        if (ModelState.IsValid)
        {
            ApiUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
                if (!result.Succeeded)
                    _logger.LogError($"{result}");
            }
        }
        return Ok();
    }

    [HttpDelete("/delete/user/role")]
    public async Task<IActionResult> DeleteUserRole([FromBody] RoleModifcation model)
    {
        IdentityResult result;
        ApiUser user = await _userManager.FindByIdAsync(model.UserName);
        if (user != null)
        {
            result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
                _logger.LogError($"{result}");
        }
        return Ok();
    }
}
