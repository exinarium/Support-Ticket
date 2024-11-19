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
using SupportTicket.SDK.Models;

namespace SupportTicket.API.Domain.Services;

public class AuthService(DataContext context, IOptions<JwtConfig> config) : IAuthService
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
}