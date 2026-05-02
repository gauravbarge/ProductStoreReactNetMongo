using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMongoCollection<User> _users;

    public AuthController(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        var usersCollection = configuration["MongoDb:UsersCollection"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        _users = database.GetCollection<User>(usersCollection);
    }

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
}