
namespace AthenasAcademy.Services.Core.Models
{
    public class InscricaoModel
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public DateTime DataInscricao { get; set; }
        public decimal Valor { get; set; }
    }
}
