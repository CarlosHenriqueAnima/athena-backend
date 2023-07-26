using AthenasAcademy.Services.API.Extensions;
using AthenasAcademy.Services.Core.Configurations.Mappers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddReadCredentials();

builder.Services.AddControllers();

builder.Services.AddAthenasServicesDI();// Adicionando Services

builder.Services.AddAthenasRepositoriesDI();// Adicionando Repositories

builder.Services.AddAWSBucketS3();

builder.Services.AddSingleton<IObjectConverter, ObjectConverter>();

builder.Services.AddPoliciesCors(); // Aciciona as pol�ticas de CORS

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documenta��o

builder.Services.AddSwaggerAutenticacaoJwtBearer(); // Adicionando configura��o JWT Tokens no swagger

builder.Services.AddConfiguracaoRestClient(builder.Configuration); // Adiciona Client 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRotasLowerCase();

builder.Services.AddAuthentication(builder.Configuration); // Adicionando configura��o JWT Tokens

builder.Services.AddAuthorization(); // Adicionando policies de admin e usuario

builder.Services.AddHttpContextAccessor();

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
