// src/CleanPOS.WebAPI/Controllers/AuthController.cs
namespace CleanPOS.WebAPI.Controllers;

using CleanPOS.Domain.Enums;
using CleanPOS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly JwtTokenService _tokenService;

    public AuthController(
        UserManager<AppIdentityUser> userManager,
        JwtTokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var user = new AppIdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Assigner le rôle
        await _userManager.AddToRoleAsync(user, request.Role.ToString());

        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials." });

        var passwordValid = await _userManager
            .CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            return Unauthorized(new { message = "Invalid credentials." });

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id, user.Email!, roles);

        return Ok(new
        {
            token,
            email = user.Email,
            roles,
            expiration = DateTime.UtcNow.AddMinutes(60)
        });
    }
}

public record RegisterRequest(
    string FullName,
    string Email,
    string Password,
    UserRole Role
);

public record LoginRequest(
    string Email,
    string Password
);