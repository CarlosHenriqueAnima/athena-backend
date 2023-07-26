using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Responses
{
    public class MatriculaResponse
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public int DetalheContratoId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string CodigoMatricula { get; set; }

    }
}
