using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Memo.Auth.Config;
using Memo.Auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Auth.Services;

/// <summary>Сервис по работе с JWT</summary>
public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>Генерирует Jwt для юзера</summary>
    public string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                // Id юзера
                new Claim("UserId", user.Id.ToString()),
                // Его роль
                new Claim("Role", ((byte)user.Role).ToString())
            }),
            // Длительность жизни токена
            Expires = DateTime.UtcNow.AddMinutes(_options.DurationInMinutes),
            // Потребитель
            Audience = _options.Audience,
            // Издатель
            Issuer = _options.Issuer,
            // Подпись
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}