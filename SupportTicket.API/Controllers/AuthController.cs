using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.API.Domain.Services;
using SupportTicket.SDK.Models;
using SupportTicket.SDK.Models.Requests.Auth;

namespace SupportTicket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<AuthResult> Login([FromBody] LoginRequest request)
    {
        return await authService.Login(request.Email, request.Password);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<JsonResult> SendForgetPasswordEmail([FromBody] LoginRequest request)
    {
        await authService.SendForgetPasswordResetEmail(request.Email);
        return new JsonResult("Password reset email sent");
    }
}