using AthenasAcademy.Services.API.Extensions;
using AthenasAcademy.Services.Core.Configurations.Mappers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAthenasServicesDI();// Adicionando Services

builder.Services.AddAthenasRepositoriesDI();// Adicionando Repositories

builder.Services.AddSingleton<IObjectConverter, ObjectConverter>();

builder.Services.AddPoliciesCors(); // Aciciona as políticas de CORS

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documentação

builder.Services.AddSwaggerAutenticacaoJwtBearer(); // Adicionando configuração JWT Tokens no swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigureLowerCaseRoutes();

builder.Services.AddAuthentication(builder.Configuration); // Adicionando configuração JWT Tokens

builder.Services.AddAuthorization(); // Adicionando policies de admin e usuario

builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUIDoc("API Athenas Academy"); // Configura suporte a documentação

app.UseHandleException(); // Configura tratamento de excecao global

app.UseSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
