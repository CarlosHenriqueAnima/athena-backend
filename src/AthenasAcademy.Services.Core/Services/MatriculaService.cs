using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services;

public class MatriculaService : IMatriculaService
{
    public MatriculaService()
    {
        
    }

    public async Task<MatriculaStatusResponse> MatricularAluno(int inscricao)
    {
        // validar se inscricao existe
        // await ValidarInscricao(inscricao);

        // validar se boleto foi pago

        // validar se contrato foi assinado

        return await Task.FromResult(new MatriculaStatusResponse
        {
            Matricula  = 0,
            Contrato = 0,
            BoletoPago = false,
            ContratoAssinado = false,
            Menssagem = string.Empty
        });
    }

    public Task<DetalheMatriculaAlunoModel> ObterDetalhesMatricula(int matricula)
    {
        throw new NotImplementedException();
    }
}