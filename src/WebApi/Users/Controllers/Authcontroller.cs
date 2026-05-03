using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using MongoDB.Driver;
using WebApi.Dtos;
using WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMongoCollection<User> _users;
    private readonly  string jwtKey;
    private readonly  string jwtIssuer;
    private readonly  string jwtAudience;

    public AuthController(IConfiguration configuration)
    {
        jwtKey= configuration["Jwt:Key"];
        jwtIssuer= configuration["Jwt:Issuer"];
        jwtAudience= configuration["Jwt:Audience"];

        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        var usersCollection = configuration["MongoDb:UsersCollection"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        _users = database.GetCollection<User>(usersCollection);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("All fields are required.");
        }

        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest("Password and confirm password do not match.");
        }

        var existingUser = await _users
            .Find(u => u.Email == request.Email || u.Username == request.Username)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return BadRequest("Username or email already exists.");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _users.InsertOneAsync(user);

        return Ok(new
        {
            message = "User registered successfully",
            userId = user.Id
        });
    }

    [AllowAnonymous]
    [HttpGet("CheckUserName")]
    public async Task<bool> CheckUserName(string username)
    {
        // Make sure to check the Username field, not the Email field
        var existingUser = await _users
            .Find(u => u.Username == username)
            .FirstOrDefaultAsync();

        // Simply return whether the user was found or not
        return existingUser != null;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        string token ="";

         var existingUser = await _users
            .Find(u => u.Username == userLogin.Username)
            .FirstOrDefaultAsync();

        if (existingUser == null) {
            return BadRequest("Login Failed");
        }
        if (existingUser !=null 
            && existingUser.PasswordHash == BCrypt.Net.BCrypt.HashPassword(userLogin.Password))
        {
            return BadRequest("Login Failed");
        }

        return Ok(new
        {
            token = GenerateToken(userLogin.Username),
            message ="Login Successfull"

        });
    }

     // To generate token
        private string GenerateToken( string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,username),
            };
            var token = new JwtSecurityToken(jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
}