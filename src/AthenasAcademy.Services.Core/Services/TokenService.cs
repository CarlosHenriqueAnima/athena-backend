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

    public async Task<UsuarioTokenModel> GerarTokenAsync(UsuarioModel usuario)
    {
        // Gera Token
        string token = await Task.FromResult(GerarToken(usuario));

        // Gera Model
        return new UsuarioTokenModel()
            {
                Atenticado = true,
                Token = token,
                Menssagem = "Token JWT OK"
            };
    }

    public string GerarToken(UsuarioModel usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenExpires = GerarTokenExpires();
        var tokenClaims = GerarClaims(usuario);
        var tokenCredentials = GerarTokenCredentials();
        var tokenDescriptor = GerarTokenDescriptor(tokenExpires, tokenCredentials, tokenClaims);

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GenerateClaims(string usuario, Role perfil)
    {

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(nameof(Role), nameof(Role.Usuario))
        };

        if (perfil == Role.Administrador)
            claims.Add(new Claim(nameof(Role), nameof(Role.Administrador)));


        return claims;
    }

    private DateTime GerarTokenExpires()
    {
        var expireHours = double.Parse(_configuration["TokenConfiguration:ExpireHours"]);
        return DateTime.UtcNow.AddHours(expireHours);
    }

    private ClaimsIdentity GerarClaims(UsuarioModel usuario)
    {
        return new(new[]
        {
            new Claim(type: ClaimTypes.Name, value: usuario.Usuario),
            new Claim(type: ClaimTypes.Role, value: usuario.Perfil.ToString()),
        });
    }

    private SigningCredentials GerarTokenCredentials()
    {
        string key = _configuration["Jwt:Key"];
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private SecurityTokenDescriptor GerarTokenDescriptor(DateTime tokenExpires, SigningCredentials tokenCredentials, ClaimsIdentity tokenClaims)
    {
        return new SecurityTokenDescriptor()
        {
            Subject = tokenClaims,
            Expires = tokenExpires,
            SigningCredentials = tokenCredentials
        };
    }
}