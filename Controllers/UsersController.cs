using Auth.Model;
using Auth.Request;
using Auth.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService,IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        //[Authorize(Policy = "Admin")]
        
        [HttpGet]
        [Authorize (Policy = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Register(AddUser user)
        {
            var newUser = _mapper.Map<User>(user);
            //newUser.role = "Admin";
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            var result = await _userService.Register(newUser);
            return CreatedAtAction(nameof(Register), result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUser user)
        {
           var existingUser = await _userService.GetUserByEmail(user.email);
            if (existingUser == null)
            {
                return NotFound("User Not Found");
            }
            if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return BadRequest("Invalid Credentials");
            }
            var token = CreateToken(existingUser);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("NameIdentifier",user.Id.ToString()),
                new Claim("Name",user.Username),
                new Claim("Role", user.role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecurity:SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                               _configuration.GetValue<string>("TokenSecurity:Issuer"),
                               _configuration.GetValue<string>("TokenSecurity:Audience"),
                               claims: claims,
                               expires: DateTime.Now.AddDays(30),
                               signingCredentials: creds);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }



    }
}
