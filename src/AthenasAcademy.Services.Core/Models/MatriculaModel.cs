namespace AthenasAcademy.Services.Core.Models
{
    public class MatriculaModel
<<<<<<< HEAD
        {
            public int Id { get; set; }
            public int AlunoId { get; set; }
            public DateTime DataMatricula { get; set; }
            public string Curso { get; set; }
        }
}


=======
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
>>>>>>> dev
