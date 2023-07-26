using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;


namespace AthenasAcademy.Services.Core.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly IDbConnection _connection;

        public MatriculaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<MatriculaModel> GetMatriculaById(int id)
        {
            try
            {

                string query = @"SELECT 
                                    id, 
                                    contrato_id ContratoId,
                                    id_detalhe_contrato DetalheContratoId,
                                    ativo,
                                    data_cadastro DataCadastro,
                                    data_alteracao DataAlteracao,
                                    codigo_matricula CodigoMatricula
                                 FROM contrato
                                 WHERE id = @Id";

                return await _connection.QueryFirstOrDefaultAsync<MatriculaModel>(query, new { Id = id });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MatriculaModel>> GetAllMatriculas()
        {
            try
            {
                string query = @"SELECT 
                                    id, 
                                    contrato_id ContratoId,
                                    id_detalhe_contrato DetalheContratoId,
                                    ativo,
                                    data_cadastro DataCadastro,
                                    data_alteracao DataAlteracao,
                                    codigo_matricula CodigoMatricula
                                 FROM contrato";

                return await _connection.QueryAsync<MatriculaModel>(query);
            }
            catch (Exception)
            {
                return new List<MatriculaModel>();
            }
        }

        public async Task CreateMatricula(MatriculaModel matricula)
        {
            try
            {
                string insertQuery = @"INSERT INTO contrato 
                                        (contrato_id, id_detalhe_contrato, ativo, data_cadastro, data_alteracao, codigo_matricula)
                                        VALUES (@ContratoId, @DetalheContratoId, @Ativo, @DataCadastro, @DataAlteracao, @CodigoMatricula)";

                await _connection.ExecuteAsync(insertQuery, matricula);
            }
            catch (Exception)
            {
                // Lidar com exceção ou log de erro.
            }
        }

        public async Task UpdateMatricula(MatriculaModel matricula)
        {
            try
            {
                string updateQuery = @"UPDATE contrato 
                                        SET contrato_id = @ContratoId, 
                                            id_detalhe_contrato = @DetalheContratoId,
                                            ativo = @Ativo,
                                            data_alteracao = @DataAlteracao,
                                            codigo_matricula = @CodigoMatricula
                                        WHERE id = @Id";

                await _connection.ExecuteAsync(updateQuery, matricula);
            }
            catch (Exception)
            {
                // Lidar com exceção ou log de erro.
            }
        }

        public async Task DeleteMatricula(int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM contrato WHERE id = @Id";
                await _connection.ExecuteAsync(deleteQuery, new { Id = id });
            }
            catch (Exception)
            {
                // Lidar com exceção ou log de erro.
            }
        }

        MatriculaModel IMatriculaRepository.GetMatriculaById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<MatriculaModel> IMatriculaRepository.GetAllMatriculas()
        {
            throw new NotImplementedException();
        }

        void IMatriculaRepository.CreateMatricula(MatriculaModel matricula)
        {
            throw new NotImplementedException();
        }

        void IMatriculaRepository.UpdateMatricula(MatriculaModel matricula)
        {
            throw new NotImplementedException();
        }

        void IMatriculaRepository.DeleteMatricula(int id)
        {
            throw new NotImplementedException();
        }

    }
}
