using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PropertiesListings.Authentication;
using PropertiesListings.Dtos;
using PropertiesListings.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PropertiesListings.Services;

namespace PropertiesListings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IMailService mailService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMailService mailService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.mailService = mailService;
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

        //User login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new UserManagerResponse { Status = "Error", Message = "User with email does not exist", IsSuccess = false }
                    );
            }

            var checkPwd = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user != null && checkPwd)
            {
               var claims = new[]
               {
                    new Claim("Email", loginDto.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
               // new MailRequest($"{user.Email}", "New title", "Login authentication done");
                return Ok(new UserManagerResponse
                {
                    Message = tokenString,
                    IsSuccess = true,
                    ExpireDate = token.ValidTo,
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Invalid email or password",
                    IsSuccess = false
                });

                
            }
        }
    }
}
