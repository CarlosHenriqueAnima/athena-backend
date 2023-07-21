using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Core.Repositories
{
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
    }
}
