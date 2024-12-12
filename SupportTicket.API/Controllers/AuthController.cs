using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

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
    public async Task<MessageResponse> SendForgetPasswordEmail([FromBody] PasswordResetEmailRequest request)
    {
        await authService.SendForgetPasswordResetEmail(request.Email);
        return new MessageResponse("Password reset email sent");
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<PasswordResetResult> ResetPassword([FromBody] PasswordResetRequest request)
    {
        var result = await authService.ResetPassword(request.Token, request.Password);

        if (result)
        {
            return new PasswordResetResult(result, "Password successfully reset");
        }
        else
        {
            return new PasswordResetResult(result, "Password reset could not be completed at this time");
        }
    }
}