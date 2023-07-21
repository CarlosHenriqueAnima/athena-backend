using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AthenasAcademy.Services.Core.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UsuarioTokenModel> GerarTokenAsync(UsuarioModel user)
    {
        var expires = GenerateTokenExpiration();

        // Gera Token
        JwtSecurityToken jwtToken = new(
            issuer: _configuration["TokenConfiguration:Issuer"],
            audience: _configuration["TokenConfiguration:Audience"],
            claims: GenerateClaims(user.Usuario, user.Tipo),
            expires: expires,
            signingCredentials: GenerateSymmetricSigningCredentials());

        // Gera Model
        return await Task.FromResult(
            new UsuarioTokenModel()
            {
                Atenticado = true,
                Token = "bearer " + new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Expira = expires,
                Menssagem = "Token JWT OK"
            });
    }

    private Claim[] GenerateClaims(string userName, Role Tipo)
    {

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(ClaimTypes.NameIdentifier, userName),
            new Claim(ClaimTypes.Role, Tipo.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return claims;
    }

    private SigningCredentials GenerateSymmetricSigningCredentials()
    {
        string key = _configuration["Jwt:Key"];
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private DateTime GenerateTokenExpiration()
    {
        var expireHours = double.Parse(_configuration["TokenConfiguration:ExpireHours"]);
        return DateTime.UtcNow.AddHours(expireHours);
    }
}