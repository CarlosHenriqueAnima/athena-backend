﻿using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IUsuarioService
{
    Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario);

    Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest loginUsuario);
}