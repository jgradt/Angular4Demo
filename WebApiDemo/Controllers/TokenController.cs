using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiDemo.Data.Dto;
using WebApiDemo.Infrastructure.Configuration;

namespace WebApiDemo.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {

        private readonly AppConfig appConfig;

        public TokenController(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LoginDto loginDto)
        {
            if (IsValidUserAndPasswordCombination(loginDto.UserName, loginDto.Password))
                return new ObjectResult(new { Token = GenerateToken(loginDto.UserName) });
            return BadRequest();
        }

        private bool IsValidUserAndPasswordCombination(string username, string password)
        {
            //TODO: implement this
            return true;
        }

        private object GenerateToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig.Settings.Jwt.SecretKey));

            //TODO: send back retrieved claims
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "John"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Email, "john.doe@blah.com")
            };

            var token = new JwtSecurityToken(
                issuer: appConfig.Settings.Jwt.Issuer,
                audience: appConfig.Settings.Jwt.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }

}
