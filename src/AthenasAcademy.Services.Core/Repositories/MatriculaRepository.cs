<<<<<<< HEAD
﻿using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
=======
﻿using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
>>>>>>> 7f24942145c284dece18f132624604ac986811b1
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Repositories
{
<<<<<<< HEAD
    public class MatriculaRepository : BaseRepository, IMatriculaRepository
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IDbConnection _dbConnection;

        public MatriculaRepository(IConfiguration configuration, IAlunoRepository alunoRepository) : base(configuration)
        {
            _alunoRepository = alunoRepository;
            _dbConnection = GetConnection(Database.Matricula); // Corrigir para usar o banco de dados correto para matrículas
        }

        #region Matricula

        public async Task<MatriculaModel> ObterMatriculaPorIdAsync(int matriculaId)
        {
            // Implementar a lógica para obter a matrícula por ID usando o Dapper
            var query = "SELECT * FROM Matricula WHERE Id = @MatriculaId";
            return await _dbConnection.QueryFirstOrDefaultAsync<MatriculaModel>(query, new { MatriculaId = matriculaId });
        }

        public async Task<IEnumerable<MatriculaModel>> ObterTodasMatriculasAsync()
        {
            // Implementar a lógica para obter todas as matrículas usando o Dapper
            var query = "SELECT * FROM Matricula";
            return await _dbConnection.QueryAsync<MatriculaModel>(query);
        }

        public async Task AdicionarMatriculaAsync(MatriculaModel matricula)
        {
            try
            {
                // Verificar se o aluno associado à matrícula existe
                var alunoExistente = await _alunoRepository.ObterAlunoPorIdAsync(matricula.AlunoId);
                if (alunoExistente == null)
                {
                    throw new ArgumentException("O aluno associado à matrícula não foi encontrado.");
                }

                // Implementar a lógica para adicionar a matrícula usando o Dapper
                var query = "INSERT INTO Matricula (aluno_id, data_matricula, curso) VALUES (@AlunoId, @DataMatricula, @Curso)";
                await _dbConnection.ExecuteAsync(query, matricula);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar a matrícula.", ex);
            }
        }

        public async Task AtualizarMatriculaAsync(MatriculaModel matricula)
        {
            try
            {
                // Verificar se o aluno associado à matrícula existe
                var alunoExistente = await _alunoRepository.ObterAlunoPorIdAsync(matricula.AlunoId);
                if (alunoExistente == null)
                {
                    throw new ArgumentException("O aluno associado à matrícula não foi encontrado.");
                }

                // Implementar a lógica para atualizar a matrícula usando o Dapper
                var query = "UPDATE Matricula SET AlunoId = @AlunoId, DataMatricula = @DataMatricula, Curso = @Curso WHERE Id = @Id";
                await _dbConnection.ExecuteAsync(query, matricula);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar a matrícula.", ex);
            }
        }

        public async Task RemoverMatriculaAsync(int id)
        {
            // Implementar a lógica para remover a matrícula usando o Dapper
            var query = "DELETE FROM Matricula WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        #endregion
=======
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly IConfiguration _configuration;

        public MatriculaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Implemente os métodos da interface IMatriculaRepository
        // para acessar e manipular os dados de matrícula no banco de dados.
        // Exemplo:
        // public async Task<MatriculaModel> ObterMatriculaPorIdAsync(int id)
        // {
        //     using (IDbConnection connection = GetConnection(Database.Matricula))
        //     {
        //         var query = "SELECT * FROM matricula WHERE id = @Id";
        //         return await connection.QueryFirstOrDefaultAsync<MatriculaModel>(query, new { Id = id });
        //     }
        // }
>>>>>>> 7f24942145c284dece18f132624604ac986811b1
    }
}
