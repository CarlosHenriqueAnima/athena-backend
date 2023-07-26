using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Requests
{
    public class MatriculaRequest
    {

        public int ContratoId { get; set; }
        public int DetalheContratoId { get; set; }
        public string CodigoMatricula { get; set; }

    }
}
