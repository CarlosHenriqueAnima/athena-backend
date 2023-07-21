using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories
{
    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(IConfiguration configuration) : base(configuration) { }

        #region Aluno

        public async Task<AlunoModel> ObterAlunoPorIdAsync(int id)
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                var query = "SELECT * FROM Alunos WHERE Id = @Id";
                //return await connection.QueryFirstOrDefaultAsync<AlunoModel>(query, new { Id = id });
            }
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AlunoModel>> ObterTodosAlunosAsync()
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                //var query = "SELECT * FROM Alunos";
               // return await connection.QueryAsync<AlunoModel>(query);
            }
            throw new NotImplementedException();
        }

        public async Task AdicionarAlunoAsync(AlunoModel aluno)
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                // await connection.ExecuteAsync("INSERT INTO Alunos (Nome, Sobrenome, Sexo, DataNascimento, Email, DataInscricao, Ativo) VALUES (@Nome, @Sobrenome, @Sexo, @DataNascimento, @Email, @DataInscricao, @Ativo)", aluno);
               
            }
        }

        public async Task AtualizarAlunoAsync(AlunoModel aluno)
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                // await connection.ExecuteAsync("UPDATE Alunos SET Nome = @Nome, Sobrenome = @Sobrenome, Sexo = @Sexo, DataNascimento = @DataNascimento, Email = @Email, DataInscricao = @DataInscricao, Ativo = @Ativo WHERE Id = @Id", aluno);
                
            }
        }

        public async Task RemoverAlunoAsync(int id)
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                // await connection.ExecuteAsync("DELETE FROM Alunos WHERE Id = @Id", new { Id = id });
                
            }
        }

        #endregion
    }
}
