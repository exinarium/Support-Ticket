using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.API.Domain.Services;
using SupportTicket.SDK.Models;

namespace SupportTicket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<AuthResult> Login([FromBody] LoginRequest request)
    {
        return await authService.Login(request.Email, request.Password);
    }
}