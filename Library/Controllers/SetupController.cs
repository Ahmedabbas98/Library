using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetupController> _logger;

        public SetupController(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<SetupController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllRole()
        {
            return Ok(_roleManager.Roles.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string Role)
        {
            //check if the role exist
            var existRole = await _roleManager.RoleExistsAsync(Role);
            if (!existRole)  //check on role exist status
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(Role));
                //We need to check if the role has been added Succeeded
                if (result.Succeeded)
                {
                    _logger.LogInformation($"The Role {Role} has been added Successfuly");
                    return Ok(new
                    {
                        Result = $"The Role {Role} has been added Successfuly"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {Role} has not been added");
                    return Ok(new
                    {
                        Result = $"The Role {Role} has not been added"
                    });
                }
            }
            return BadRequest(new { Error = "Role already exist" });

        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_userManager.Users.ToList());
        }
        [HttpPost]
        [Route("AddUserTORole")]
        public async Task<IActionResult> AddUserTORole(string email, string RoleName)
        {
            //check if has User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) //Check User does not exist
            {
                _logger.LogInformation($"The User {email} does not exist");
                return BadRequest(new
                {
                    Error = $"User does not exist"
                });
            }
            //check if has Role exist
            var ExistRole = await _roleManager.RoleExistsAsync(RoleName);
            if (!ExistRole) //Check on the Role does exist status
            {
                _logger.LogInformation($"The role {RoleName} does not exist");
                return BadRequest(new
                {
                    Error = $"Role does not exist"
                });
            }
            var Result = await _userManager.AddToRoleAsync(user, RoleName);
            //check if has User is assigned to the role Successfuly
            if (Result.Succeeded)
            {
                return Ok(new
                {
                    Result = $"Success,user has been added to the role"
                });
            }
            else
            {
                _logger.LogInformation($"The user was not able to be added to the role");
                return BadRequest(new
                {
                    Error = $"The user was not able to be added to the role"
                });
            }

        }
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            //check if has User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) //Check User does not exist
            {
                _logger.LogInformation($"The User {email} does not exist");
                return BadRequest(new
                {
                    Error = $"User does not exist"
                });
            }
            //return the Role
            var Role = await _userManager.GetRolesAsync(user);
            return Ok(Role);
        }
        [HttpPost]
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string RoleName)
        {

            //check if has User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) //Check User does not exist
            {
                _logger.LogInformation($"The User {email} does not exist");
                return BadRequest(new
                {
                    Error = $"User does not exist"
                });
            }
            //check if has Role exist
            var ExistRole = await _roleManager.RoleExistsAsync(RoleName);
            if (!ExistRole) //Check on the Role does exist status
            {
                _logger.LogInformation($"The role {RoleName} does not exist");
                return BadRequest(new
                {
                    Error = $"Role does not exist"
                });
            }
            var Result = await _userManager.RemoveFromRoleAsync(user, RoleName);
            if (Result.Succeeded)
            {
                return Ok(new
                {
                    Result = $"User {email} has been Removeed from role {RoleName}"
                });
            }
            return BadRequest(new
            {
                Error = $"Unable to remove User {email} from Role {RoleName} "
            });
        }
    }
}
