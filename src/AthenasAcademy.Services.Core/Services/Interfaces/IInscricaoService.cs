using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IInscricaoService
{
    Task<CandidatoResponse> CadastrarCandidato(NovoCandidatoRequest request);
}