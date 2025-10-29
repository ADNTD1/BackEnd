using BackEnd.DTOs;
using BackEnd.Models;
using Ecomerce_Back_End.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        /* -------------------------------------------------------------------------- */

        [HttpPost("/login")]


        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            var User = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (User == null) { return Unauthorized("Usuario no encontrado"); }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, User.PasswordHash)) {  // si el usuario mando su correo y si existe se comparan las contrasenas 
            return Unauthorized("Contraseña incorrecta"); // si no es correcta se manda un no autorizado
            }
            var token = GenerateJwtToken(User);

            return Ok(new  
            {
                token,
                user = new { User.Id, User.Email, User.FullName }
            });
        }

        private string GenerateJwtToken(User user) 
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullname", user.FullName ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // se genera el jwt y se retorna
        }

        /* -------------------------------------------------------------------------- */
        [HttpPost("/register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto NewUser)
        {
            if (NewUser == null) // en caso de contrasena nula
            {
                throw new ArgumentNullException(nameof(NewUser));
            }

            if (await _context.Users.AnyAsync(u => u.Email == NewUser.Email)) {  // si el email de la request ya se encuentra en la bd se retorna un badRequest
                return BadRequest("el correo ya esta registrado");
            }

            string HashedPass = BC.HashPassword(NewUser.Password); // se hashea la contrasena antes d emeterse a la bd

            var user = new User  // de la instacia del Dto se meten los datos al modelo que se usa en la BD
            {   
                FullName = NewUser.FullName,
                CreatedAt = DateTime.UtcNow,
                Email = NewUser.Email,
                PasswordHash = HashedPass

            };
            _context.Users.Add(user); 
            await _context.SaveChangesAsync(); //  se guarda todo en la base de datos y se manda un 200 OK para saber que se hizo exitosamente 
            return Ok("registro exitoso");
        }   
                

        
    }

}

