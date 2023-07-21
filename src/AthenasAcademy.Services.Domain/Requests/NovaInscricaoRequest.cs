using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Requests
{
    public class NovaInscricaoRequest
    {
        public int AlunoId { get; set; }

        public DateTime DataInscricao { get; set; }

        public decimal Valor { get; set; }
    }
}
