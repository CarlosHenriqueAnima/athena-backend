using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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

    public async Task<TokenModel> GerarTokenUsuario(UsuarioTokenModel usuario)
    {
        (string token, DateTime validade) = GerarToken(usuario);

        return await Task.FromResult<TokenModel>(
            new ()
            {
                Menssagem = "Token OK",
                Token = token,
                Validade = validade
            });
    }

    public (string, DateTime) GerarToken(UsuarioTokenModel usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenExpires = GerarTokenExpires();
        var tokenClaims = GerarClaims(usuario);
        var tokenCredentials = GerarTokenCredentials();
        var tokenDescriptor = GerarTokenDescriptor(tokenExpires, tokenCredentials, tokenClaims);

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), tokenExpires);
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

    private ClaimsIdentity GerarClaims(UsuarioTokenModel usuario)
    {
        try
        {
            Role perfil = (Role)usuario.Perfil;
            string usuarioPerfil = nameof(perfil);

            return new(new[]
            {
                new Claim(type: ClaimTypes.Name, value: usuario.Usuario),
                new Claim(type: ClaimTypes.Role, value: usuarioPerfil)
            });
        }
        catch (Exception ex)
        {
            throw new TokenCustomException(string.Format("Perfil {0} não é válido.", usuario.Perfil), ExceptionResponseType.Error, ex, HttpStatusCode.BadRequest);
        }
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