using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using ReactAndJwt.Dto;
using ReactAndJwt.Model;
using ReactAndJwt.Repository;
using ReactAndJwt.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactAndJwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
       private readonly IRepository _repo;
        private readonly IConfiguration _config;
        private readonly IService _service;
        public AuthorizationController(IConfiguration config,IRepository repo,IService service)
        {
            _repo = repo;
            _config = config;
            _service = service;
        }
        [HttpPost("register")]
        public IActionResult Register(UserRequestDto Req)
        {

            var check = _repo.Get(Req);
            if (check != null) return BadRequest();
            var UserName = Req.UserName;
            var role = Req.Role;
            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(Req.Password);
            _repo.Add(new User { UserName = UserName, HashedPassword = HashedPassword ,Role=role});
            return Ok("User Registered Successfully");


        }

        [HttpPost("login")]
        public IActionResult Login(UserRequestDto req)
        {

            var user = _repo.Get(req);
            if (user is null) return BadRequest("User not exist");
            if (!(BCrypt.Net.BCrypt.Verify(req.Password,user.HashedPassword)))
            {
                return BadRequest("Password is wrong");
            }
            return Ok(new { token = CreateToken(user) });


        }
        [HttpPost("check-credit")]
        public IActionResult Check(ApplicationTakingDto req)
        {

            var parameters = _service.Check();
            return Ok(parameters);
        }

        [HttpPost("apply")]
        public IActionResult Apply(ApplicationTakingDto req) { 
        
            var userid=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userid == null)
            {
                return Unauthorized("UserId not found in token");
            }

            _service.Application(req,userid);

            return Ok("Added Successfullly");
        }
        public string CreateToken(User user)
        {
            var claim = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("role", user.Role)
            };

            var jwt = _config.GetSection("jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
             var token = new JwtSecurityToken(
                 issuer: jwt["Issuer"],
                 audience: jwt["Audience"],
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
    

}
