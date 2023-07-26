using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IInscricaoRepository
{
    Task<InscricaoCandidatoModel> RegistrarNovaInscricao(InscricaoCandidatoArgument argument);
}