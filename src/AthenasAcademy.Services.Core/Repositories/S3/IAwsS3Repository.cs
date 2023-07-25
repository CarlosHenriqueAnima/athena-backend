namespace AthenasAcademy.Services.Core.Repositories.S3;

public interface IAwsS3Repository
{
    Task<MemoryStream> ObterPDFAsync(string recurso);
}