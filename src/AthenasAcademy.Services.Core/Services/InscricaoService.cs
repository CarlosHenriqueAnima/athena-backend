<<<<<<< HEAD
﻿using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
=======
﻿using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using System.Net;
>>>>>>> dev

namespace AthenasAcademy.Services.Core.Services
{
<<<<<<< HEAD
    public class InscricaoService : IInscricaoService
    {
        public Task<Interfaces.InscricaoResponse> AtualizarInscricao(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarInscricao(InscricaoModel inscricao)
        {
            throw new NotImplementedException();
        }

        public Task<NovaInscricaoResponse> CadastrarInscricao(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarInscricao(InscricaoModel inscricao)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesativarInscricao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Interfaces.InscricaoResponse> ObterInscricao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Interfaces.InscricaoResponse>> ObterTodasInscricoes()
        {
            throw new NotImplementedException();
        }
=======
    private readonly IInscricaoRepository _inscricaoRepository;

    public InscricaoService(IInscricaoRepository inscricaoRepository)
    {
        _inscricaoRepository = inscricaoRepository;
    }

    public async Task<CandidatoModel> ObterInscricaoPorIdAsync(int inscricaoId)
    {
        return await _inscricaoRepository.ObterInscricaoPorIdAsync(inscricaoId);
    }

    public async Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync()
    {
        return await _inscricaoRepository.ObterInscricoesPendentesAsync();
    }

    public async Task AdicionarInscricaoAsync(CandidatoModel inscricao)
    {
        if (inscricao == null)
            throw new APICustomException(string.Format("Não há dados de inscrição."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(inscricao.Email))
            throw new APICustomException(string.Format("O candidato precisa ter um email."), ExceptionResponseType.Warning, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(inscricao.Nome))
            throw new APICustomException(string.Format("Nome do candidato não pode estar em branco."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(inscricao.CursoInteresse))
            throw new APICustomException(string.Format("Informe um curso de interesse."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        await _inscricaoRepository.AdicionarInscricaoAsync(inscricao);
    }

    public async Task AtualizarInscricaoAsync(CandidatoModel inscricao)
    {
        if (inscricao == null)
            throw new APICustomException(string.Format("Não há dados de inscrição."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        if (String.IsNullOrEmpty(inscricao.Nome) || String.IsNullOrEmpty(inscricao.Email) || String.IsNullOrEmpty(inscricao.CodigoInscricao))
            throw new APICustomException(string.Format("São necessários nome, email e código de inscrição."), ExceptionResponseType.Error, HttpStatusCode.BadRequest);

        await _inscricaoRepository.AtualizarInscricaoAsync(inscricao);
    }

    public async Task CancelarInscricaoAsync(int id)
    {
        await _inscricaoRepository.CancelarInscricaoAsync(id);
>>>>>>> dev
    }
}
