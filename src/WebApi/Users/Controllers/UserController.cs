using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers;

[Authorize]
[ApiController] // Add this for automatic model validation and better API behavior
[Route("api/[controller]")] // Sets the route to /api/user
public class UserController : ControllerBase
{
    private readonly IUserService _userService; // Use private readonly for better practice

    public UserController(IUserService userService)
    {   
        _userService = userService;
    }

    [HttpGet("GetUsers")] // This defines the HTTP method
    public async Task<IActionResult> GetUsers()
    {
        var users = _userService.GetUsers(); // Ensure you 'await' if the service is async
        return Ok(users);
    }
}
