using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AthenasAcademy.Services.Core.Exceptions;

namespace AthenasAcademy.Services.Core.Repositories.S3;

public class AwsS3Repository : IAwsS3Repository
{
    private readonly IConfiguration _configuration;

    public AwsS3Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<MemoryStream> ObterPDFAsync(string recurso)
    {
        string bucket = _configuration.GetValue<string>("AWS:S3:BucketBase");

        try
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucket,
                Key = recurso
            };

            using (GetObjectResponse response = await GetClient().GetObjectAsync(request))
            {
                MemoryStream memoryStream = new MemoryStream();
                await response.ResponseStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
        catch (AmazonS3Exception ex)
        {
            throw new APICustomException($"Erro ao acessar o arquivo: {ex.Message}",
                Domain.Configurations.Enums.ExceptionResponseType.Error,
                System.Net.HttpStatusCode.InternalServerError);
        }
    }

    private AmazonS3Client GetClient()
    {
        try
        {
            string accessKey = _configuration.GetValue<string>("AWS:S3:AccessKey");
            string secretKey = _configuration.GetValue<string>("AWS:S3:SecretKey");

            BasicAWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

            AmazonS3Config config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USWest2
            };

            return new AmazonS3Client(credentials, config);
        }
        catch (AmazonS3Exception ex)
        {
            throw;
        }
    }

}