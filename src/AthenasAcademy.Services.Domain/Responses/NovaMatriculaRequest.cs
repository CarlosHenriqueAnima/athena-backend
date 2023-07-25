using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Responses
{
    public class NovaMatriculaRequest
    {
        public int AlunoId { get; set; }
        public string Curso { get; set; }
    }
}
