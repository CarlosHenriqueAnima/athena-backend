namespace AthenasAcademy.Services.Core.Configurations.Credentials
{
    public class Credentials
    {
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string AwsBucketBase { get; set; }
        public string ClientLegadoAwsSecretKey { get; set; }
        public string JwtSecretKey { get; set; }

        public string StringConnectionAthenasBase { get; set; }
        
        public string StringConnectionBaseUsuario {get; set;}
        public string StringConnectionBaseInscricao {get; set;}
        public string StringConnectionBaseAluno { get; set; }
        public string StringConnectionBaseMatricula { get; set; }
        public string StringConnectionBasePagamento { get; set; }
        public string StringConnectionBaseCurso { get; set; }
        public string StringConnectionBaseCertificado { get; set; }
    }
}
