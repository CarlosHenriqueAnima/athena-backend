using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Dapper;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync();
            string query = @"SELECT 
                                    id,
                                    usuario, 
                                    senha_hash AS Senha, 
                                    id_perfil AS Perfil,
                                    ativo
                                FROM usuario
                                WHERE usuario = @Usuario OR email = @Usuario";

            return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Usuario = novoUsuario.Email });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<UsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync();
            string query = @"INSERT INTO usuario (usuario, email, senha_hash, ativo, data_cadastro, data_alteracao, id_perfil)
                             VALUES (@Usuario, @Email, @Senha, @Ativo, @DataCadastro, @DataAlteracao, 2)
                             RETURNING id, usuario, senha_hash AS Senha, id_perfil AS Perfil";

            return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, novoUsuario);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }


    public async Task<IEnumerable<DadosUsuarioModel>> ObterDadosCompletosUsuario(string usuario)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync();
            string query = @"select 
	                            u.id as IdUsuario,
	                            u.email as Login,
	                            u.data_cadastro as DataCadastroUsuario,
	                            al.nome || ' ' || al.sobrenome as NomeAluno,
	                            al.cpf,
	                            al.sexo,
	                            al.data_nascimento as DataNascimento,
	                            da.codigo_inscricao as CodigoInscricao,
	                            ic.data_inscricao as DataInscricao,
	                            ic.boleto_pago as BoletoPago,
	                            ic.dir_boleto_inscricao as DiretorioBoleto,
	                            c.nome as NomeCurso,
	                            c.descricao as DescricaoCurso,
	                            c.carga_horaria as CargaHorariaCurso,
	                            ac.nome as AreaDoConhecimento,
	                            d.nome as Disciplina,
	                            d.descricao as DescricaoDisciplina,
	                            d.carga_horaria as CargaHorariaDisciplina,
	                            ma.matricula,
	                            ma.ativa,
	                            ma.data_matricula,
	                            cma.contrato,
	                            cma.assinado,
	                            cma.forma_pagamento as FormaPagamento,
	                            cma.valor_contrato as ValorPagamento,
	                            cma.data_aceite as DataAceite,
	                            cma.dir_contrato_matricula as DiretorioContrato,
	                            cma.data_aceite as DataAceite,
	                            c2.aproveitamento,
	                            c2.data_conclusao as DataConclusao,
	                            c2.gerado,
	                            c2.caminho_certificado_pdf as DiretorioCertificadoPDF,
	                            c2.caminho_certificado_png as DiretorioCertificadoPNG
                            from
	                            usuario u
                            left join aluno al on
	                            al.email = u.usuario
                            left join detalhe_aluno da on
	                            da.id_aluno = al.id
                            left join inscricao_candidato ic on
	                            ic.codigo_inscricao = da.codigo_inscricao
                            left join curso c on
	                            c.id = ic.codigo_curso::integer
                            left join area_conhecimento ac on
	                            ac.id = ic.codigo_curso::integer
                            left join disciplina d on
	                            d.id_curso = ic.codigo_curso::integer
                            left join matricula_aluno ma on
	                            ma.codigo_aluno = al.id
                            left join contrato_matricula_aluno cma on
	                            cma.id_matricula = ma.id
                            left join certificado c2 on
	                            c2.matricula::integer = ma.matricula
                            where
	                            u.usuario = @Usuario;";

            return await connection.QueryAsync<DadosUsuarioModel>(query, new { Usuario = usuario });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}