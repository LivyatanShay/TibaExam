using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TibaExam.Models;
using Microsoft.Extensions.Configuration;
using TibaExam.Interfaces;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration appSettings;

        public UsersController(IUserService userService, IConfiguration cfg)
        {
            this.userService = userService;
            appSettings = cfg;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] dynamic json)
        {
            var model = JsonConvert.DeserializeObject<AuthenticateModel>(json.ToString());
            var user = userService.Authenticate(model.Username, model.Password);

            if (user == null)
            { 
                return BadRequest(new { message = "Username or password is incorrect" }); 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
    }
}
