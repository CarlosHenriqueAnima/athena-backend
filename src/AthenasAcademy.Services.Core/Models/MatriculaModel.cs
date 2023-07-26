namespace AthenasAcademy.Services.Core.Models
{
    public class MatriculaModel
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
