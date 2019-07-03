using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Template.Api.Configuration;
using Template.Api.Models.RequestModel;
using Template.Api.Models.ResponseModel;
using Template.Domain.Entities;
using Template.Services.UserService;

namespace Template.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private IMapper mapper;
        private readonly AppSettings appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appsettings)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.appSettings = appsettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserRequestModel userDto)
        {
            User user;
            try
            {
                user = userService.Authenticate(userDto.EmailAddress, userDto.Password);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }                

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
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

            var userToReturn = mapper.Map<UserResponseModel>(user);
            userToReturn.Token = tokenString;

            return Ok(userToReturn);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRequestModel userDto)
        {
            var user = mapper.Map<User>(userDto);

            try
            {
                userService.Create(user, userDto.Password);                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            var userDtos = mapper.Map<IList<UserRequestModel>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = userService.GetById(id);
            var userDto = mapper.Map<UserRequestModel>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserRequestModel userDto)
        {
            var user = mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                userService.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}