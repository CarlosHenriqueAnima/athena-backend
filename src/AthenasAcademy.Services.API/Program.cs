using AthenasAcademy.Services.API.Extensions;
using AthenasAcademy.Services.Core.Configurations.Credentials;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using Microsoft.Extensions.WebEncoders.Testing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAthenasServicesDI();// Adicionando Services

builder.Services.AddAthenasRepositoriesDI();// Adicionando Repositories

builder.Services.AddSingleton<IObjectConverter, ObjectConverter>();

builder.Services.AddPoliciesCors(); // Aciciona as pol�ticas de CORS

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documenta��o

builder.Services.AddSwaggerAutenticacaoJwtBearer(); // Adicionando configura��o JWT Tokens no swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigureLowerCaseRoutes();

builder.Services.AddAuthentication(builder.Configuration); // Adicionando configura��o JWT Tokens

builder.Services.AddAuthorization(); // Adicionando policies de admin e usuario

builder.Services.AddHttpContextAccessor();

var credenciais = ReadCredentials.GetCredentials();

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    {"AwsAccessKey", credenciais.AwsAccessKey},
    {"AwsSecretKey", credenciais.AwsSecretKey},
    {"AwsBucketBase", credenciais.AwsBucketBase},
    {"InscricaoBase", credenciais.StringConnectionBaseInscricao},
    {"AlunoBase", credenciais.StringConnectionBaseAluno},
    {"MatriculaBase", credenciais.StringConnectionBaseMatricula},
    {"PagamentoBase", credenciais.StringConnectionBasePagamento},
    {"CursoBase", credenciais.StringConnectionBaseCurso},
    {"CertificadoBase", credenciais.StringConnectionBaseCertificado}
});

WebApplication app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUIDoc("API Athenas Academy"); // Configura suporte a documenta��o

app.UseHandleException(); // Configura tratamento de excecao global

app.UseSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
