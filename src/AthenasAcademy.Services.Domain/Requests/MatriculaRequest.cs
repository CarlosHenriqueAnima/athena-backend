using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Requests
{
    public class MatriculaRequest
    {
        public int AlunoId { get; set; }
        public DateTime DataMatricula { get; set; }
        public string Curso { get; set; }
        public int Id { get; set; }
    }
}
