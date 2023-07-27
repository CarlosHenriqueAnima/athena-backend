using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using Dapper;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class CertificadoRepository : BaseRepository, ICertificadoRepository
{
    public CertificadoRepository(IConfiguration configuration) : base(configuration)  { }

    public async Task<CertificadoModel> GerarCertificado(NovoCertificadoArgument novoCertificado)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync();
            string query = @"
                INSERT INTO certificado (
                    nome_aluno,
                    matricula,
                    nome_curso,
                    codido_curso,
                    carga_horaria_curso,
                    aproveitamento,
                    data_conclusao,
                    gerado,
                    caminho_certificado_pdf,
                    caminho_certificado_png
                )
                VALUES (
                    @NomeAluno,
                    @Matricula,
                    @NomeCurso,
                    @CodigoCurso,
                    @CargaHorariaCurso,
                    @Aproveitamento,
                    @DataConclusao,
                    @Gerado,
                    @CaminhoCertificadoPdf,
                    @CaminhoCertificadoPng
                )
                RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<CertificadoModel>(query, novoCertificado);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<CertificadoModel> ObterCertificado(int matricula)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync();
            string query = @"
                    SELECT 
                        id,
                        nome_aluno AS NomeAluno,
                        matricula,
                        nome_curso AS NomeCurso,
                        codido_curso AS CodigoCurso,
                        carga_horaria_curso AS CargaHorariaCurso,
                        aproveitamento,
                        data_conclusao AS DataConclusao,
                        gerado,
                        caminho_certificado_pdf AS CaminhoCertificadoPdf,
                        caminho_certificado_png AS CaminhoCertificadoPng
                    FROM certificado
                    WHERE matricula = @Matricula";

            return await connection.QueryFirstOrDefaultAsync<CertificadoModel>(query, new { Matricula = matricula.ToString() });
        }
        catch (Exception)
        {
            return null;
        }

    }
}