using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Task_Repo_Pattern.Interfaces;

namespace Task_Repo_Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogManager _logger;
        public LoginController(ILogManager logger)
        {
            _logger = logger;
        }
        [HttpGet, AllowAnonymous]
        [Route("token")]
        public IActionResult GetToken(string username="alpha", string password="beta")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Deb@321@secret12345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            if (username != "alpha" || password != "beta") return Unauthorized();
            var claims = new[] {
                    new Claim("Username", username)
                   };
            var tokenOptions = new JwtSecurityToken(
                issuer: "taskapi.com",
                audience: "taskapp",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            _logger.LogInfo($"Key created : {token}");
            return Ok(token);
        }
    }
}