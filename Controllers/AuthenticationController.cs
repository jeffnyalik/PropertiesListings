using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PropertiesListings.Authentication;
using PropertiesListings.Dtos;
using PropertiesListings.Helpers;

namespace PropertiesListings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthenticationController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        //Register user
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto registerDto)
        {
            //Check if the email address already exist
            var userExists = await userManager.FindByEmailAsync(registerDto.Email);
            //check username
            var usernameExists = await userManager.FindByNameAsync(registerDto.UserName);
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new UserManagerResponse { Status = "Error", Message = "User with this Email already exists" }
                    );
            }
            if(usernameExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                   new UserManagerResponse { Status = "Error", Message = "User with this Username already exists" }
                   );
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var results = await userManager.CreateAsync(user, registerDto.Password);
            if (!results.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new UserManagerResponse { Status = "Error", Message = "There is problem creating an account" }
                    );
            }
            return Ok(new UserManagerResponse {Status = "Success", Message="Created successfully", IsSuccess = true });
        }
    }
}
