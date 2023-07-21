using AthenasAcademy.Services.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutenticacaoJwtBearer(builder.Configuration); // Adicionando configuração JWT Tokens

builder.Services.AddPoliciesAutorizacao(); // Adicionando policies de admin e usuario

builder.Services.AddSwaggerAutenticacaoJwtBearer(); // Adicionando configuração JWT Tokens no swagger

builder.Services.AddServicosScoped();// Adicionando Services

builder.Services.AddRepositoriosSingleton();// Adicionando Repositories

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documentação

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddConfigureLowerCaseRoutes();

WebApplication app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerUIDoc("API Athenas Academy"); // Configura suporte a documentação

app.UseHandleException(); // Configura tratamento de excecao global

app.UseSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
