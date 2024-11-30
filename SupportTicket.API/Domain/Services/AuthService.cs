using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupportTicket.API.Domain.Config;
using SupportTicket.API.Domain.Helpers;
using SupportTicket.API.Domain.Repository.Models;
using SupportTicket.SDK.Enums;
using SupportTicket.SDK.Models;

namespace SupportTicket.API.Domain.Services;

public class AuthService(
    DataContext context,
    IOptions<JwtConfig> config,
    ILogger<AuthService> logger,
    IOptions<ServerConfig> serverConfig,
    IOptions<EmailConfig> emailConfig,
    IEmailService emailService) : IAuthService
{
    public async Task<AuthResult> Login(string email, string password)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || PasswordHasher.VerifyPassword(user.Password, password) == false)
            {
                throw new Exception("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return new AuthResult(token, true);
        }
        catch (Exception e)
        {
            return new AuthResult(null, false, e.Message);
        }
    }

    public async Task SendForgetPasswordResetEmail(string email)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null )
            {
                return;
            }

            var token = Guid.NewGuid();
            user.ResetPasswordToken = token.ToString();
            user.ResetPasswordTokenExpirationDate = DateTime.UtcNow.AddMinutes(30);

            context.Users.Update(user);
            await context.SaveChangesAsync();

            emailService.SendEmailAsync(new Email()
            {
                To = [user.Email],
                Subject = "Reset Password",
                HtmlBody = @$"<p>A password reset has been requested for your account. To reset your password <a href='{serverConfig.Value.BaseUrl}/Auth/ResetPassword'>click here</a></p>
                            <p>If you did not request this reset, please ignore this email.</p>",
                From = emailConfig.Value.FromAddress,
                FromName = emailConfig.Value.FromName,
                CreatedBy = user,
                IsSMTPEmail = true,
                EmailType = EmailType.FORGOT_PASSWORD,
                CC = [],
                BCC = [],
                ReplyTo = [emailConfig.Value.FromAddress],
                IsHtml = true,
                AttachmentFileNames = [],
                KeyValuePairs = new()
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task<bool> ResetPassword(string token, string newPassword)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == token && x.ResetPasswordTokenExpirationDate > DateTime.UtcNow);

            if (user == null )
            {
                return false;
            }

            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpirationDate = null;
            user.Password = PasswordHasher.HashPassword(newPassword);

            context.Users.Update(user);
            await context.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            throw;
        }
    }

    private string GenerateJwtToken(User user)
    {
        var issuer = config.Value.Issuer;
        var audience = config.Value.Audience;
        var expiryMinutes = config.Value.ExpiryMinutes;
        var key = Encoding.ASCII.GetBytes(config.Value.Key);

        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));

        if (!user.IsEmailConfirmed)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Unverified"));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}

public interface IAuthService
{
    Task<AuthResult> Login(string email, string password);

    Task SendForgetPasswordResetEmail(string email);

    Task<bool> ResetPassword(string token, string newPassword);
}