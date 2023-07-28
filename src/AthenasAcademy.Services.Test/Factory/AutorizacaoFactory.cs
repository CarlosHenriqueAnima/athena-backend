using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Test.Factory
{
    public class AutorizacaoFactory
    {
        public NovoUsuarioRequest ObterNovoUsuarioRequestValido()
        {
            return new NovoUsuarioRequest()
            {
                Email = "email@dominio.com",
                ConfirmEmail = "email@dominio.com",
                Senha = "dU@33D!J.4JNcA#"
            };
        }

        public NovoUsuarioArgument RetornarNovoUsuarioArgumentValido()
        {
            return new NovoUsuarioArgument()
            {
                Usuario = "usuario.login",
                Email = "email@dominio.com",
                Senha = "dU@33D!J.4JNcA#",
                Ativo = true,
                DataCadastro = DateTime.Now
            };
        }

        public UsuarioModel ObterUsuarioModelValido()
        {
            return new UsuarioModel()
            {
                Id = 1,
                Usuario = "usuario.login",
                Senha = "dU@33D!J.4JNcA#",
                Ativo = true,
                DataCadastro = RetornarNovoUsuarioArgumentValido().DataCadastro
            };
        }

        public UsuarioArgument RetornarUsuarioArgumentValido()
        {
            return new UsuarioArgument()
            {
                Email = "email@dominio.com"
            };
        }

        public List<UsuarioModel> ObterListaUsuarioModelValidos()
        {
            var dataCadastroInicial = new DateTime(2023, 07, 25);

            return Enumerable.Range(1, 2).Select(i => new UsuarioModel
            {
                Id = i,
                Usuario = $"usuario{i}.login",
                Senha = $"@5enHA{i}!",
                Ativo = i % 2 == 0,
                DataCadastro = dataCadastroInicial.AddDays(i - 1)
            }).ToList();
        }

        public TokenModel ValidarToken()
        {
            return new TokenModel()
            {
                Token = RetornarTokenValido().Token,
                Validade = DateTime.Now.AddDays(1),
                Menssagem = RetornarTokenValido().Menssagem
            };
        }

        public TokenResponse RetornarTokenValido()
        {
            return new TokenResponse()
            {
                Token = "f6725c68e3152b2cbe30d18da605e13a7cd905a73ed6a2e0982ac20206731d03",
                Validade = DateTime.Now.AddDays(1),
                Menssagem = "Token gerado para 24 horas"
            };
        }

        public NovoUsuarioResponse ObterNovoUsuarioValido()
        {
            return new NovoUsuarioResponse()
            {
                Resultado = true,
                Usuario = "usuario.login",
                Token = RetornarTokenValido(),
            };
        }

        public LoginUsuarioRequest ObterLoginUsuarioRequestValido()
        {
            return new LoginUsuarioRequest()
            {
                Usuario = "usuario.login",
                Senha = "dU@33D!J.4JNcA#"
            };
        }

        public LoginUsuarioResponse RetornarLoginUsuarioResponseValido()
        {
            return new LoginUsuarioResponse()
            {
                Resultado = true,
                Token = RetornarTokenValido()
            };
        }
    }
}
