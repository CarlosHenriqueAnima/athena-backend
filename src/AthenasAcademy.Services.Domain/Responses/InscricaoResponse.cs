<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Domain.Responses
=======
﻿namespace AthenasAcademy.Services.Domain.Responses
>>>>>>> dev
{
    public class InscricaoResponse
    {
        public int Id { get; set; }

<<<<<<< HEAD
        public int AlunoId { get; set; }

        public DateTime DataInscricao { get; set; }

        public decimal Valor { get; set; }
=======
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CodigoInscricao { get; set; }

        public string CursoInteresse { get; set; }

        public DateTime DataInscricao { get; set; }

        public string Curso { get; set; }

        public string Boleto { get; set; }
>>>>>>> dev
    }
}
