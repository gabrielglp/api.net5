using Microsoft.AspNetCore.Mvc;
using api.net5.Models.RegistrationModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using api.net5.Context;
using api.net5.Models.LoginModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace api.net5.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
        {
            if (registrationModel == null)
            {
                return BadRequest("AuthAccount object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // verifica se o email já está registrado
                bool emailAlreadyRegistered = await _context.registrationModels
                    .AnyAsync(u => u.Email == registrationModel.Email);

                if (emailAlreadyRegistered)
                {
                    return Conflict(new { message = "Email already registered." });
                }

                // criptografa a senha antes de armazenar no banco de dados
                registrationModel.Password = BCrypt.Net.BCrypt.HashPassword(registrationModel.Password);

                _context.registrationModels.Add(registrationModel);
                await _context.SaveChangesAsync();

                var response = new
                {
                    Id = registrationModel.UserId,
                    Name = registrationModel.UserName,
                    Email = registrationModel.Email
                };

                return CreatedAtAction(nameof(Register), new { id = registrationModel.UserId }, response);
            }
            catch (Exception)
            {
                return StatusCode(500, $"An error occurred on the server");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(new { message = "Login request is null." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool isEmail = loginRequest.Email.Contains('@') && loginRequest.Email.Contains('.');

                var user = await _context.registrationModels
                    .SingleOrDefaultAsync(u =>
                        (isEmail && u.Email == loginRequest.Email) ||
                        (!isEmail && u.UserName == loginRequest.Email));

                bool isUserNotFound = user == null;
                bool isPasswordInvalid = !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user?.Password);

                if (isUserNotFound || isPasswordInvalid)
                {
                    return BadRequest(new { message = "Email or password is incorrect." });
                }

                var tokenString = GenerateJwtToken(user.Email);

                var response = new
                {
                    Token = tokenString,
                    User = new
                    {
                        Id = user.UserId.ToString(),
                        Name = user.UserName,
                        Email = user.Email
                    }
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = $"An error occurred on the server" });
            }
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}