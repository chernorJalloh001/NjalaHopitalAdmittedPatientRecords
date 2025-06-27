using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NjalaHopitalAdmittedPatientRecords.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NjalaHopitalAdmittedPatientRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NjalaHopitalContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(NjalaHopitalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        // ✅ POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Registration Register)
        {
            if (string.IsNullOrWhiteSpace(Register.UserName) || string.IsNullOrWhiteSpace(Register.Password))
                return BadRequest("Username and password are required.");

            if (_context.Register.Any(u => u.UserName.ToLower() == Register.UserName.ToLower()))
                return BadRequest("Username already exists.");

            var hasher = new PasswordHasher<Registration>();
            Register.Password = hasher.HashPassword(Register, Register.Password);

            try
            {
                _context.Register.Add(Register);
                await _context.SaveChangesAsync();
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        
        // ✅ POST: api/Auth/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("Username and password are required.");

            var user = _context.Register.SingleOrDefault(u => u.UserName.ToLower() == login.UserName.ToLower());

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var hasher = new PasswordHasher<Registration>();
            var result = hasher.VerifyHashedPassword(user, user.Password, login.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password.");

            var token = GenerateJwtToken(user.UserName);

            return Ok(new
            {
                token = token,
                message = "Login successful"
            });
        }

        
        // 🔐 Protected GET: All users (Login required)
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Registration>> GetAllUsers()
        {
            return Ok(_context.Register.ToList());
        }

        

       



        // ✅ JWT Generation Helper Method
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
