using AthenasAcademy.Services.API.Extensions;
using AthenasAcademy.Services.Core.Configurations.Mappers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddReadCredentials();

builder.Services.AddControllers();

builder.Services.AddAthenasServicesDI();

builder.Services.AddAthenasRepositoriesDI();

builder.Services.AddAWSBucketS3();

builder.Services.AddSingleton<IObjectConverter, ObjectConverter>();

builder.Services.AddApiVersionamento();

builder.Services.AddApiVersionamentoExplorer();

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");

builder.Services.AddSwaggerAutenticacaoJwtBearer();

builder.Services.AddConfiguracaoRestClient(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRotasLowerCase();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

app.UseCorsConfig();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUIDoc("API Athenas Academy");

app.UseHandleException();

app.UseSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
