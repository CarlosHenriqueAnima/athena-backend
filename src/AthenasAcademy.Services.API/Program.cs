using AthenasAcademy.Services.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicosScoped();// Adicionando Services

builder.Services.AddRepositoriosSingleton();// Adicionando Repositories

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documentação

builder.Services.AddAutenticacaoJwtBearer(builder.Configuration); // Adicionando configuração JWT Tokens

builder.Services.AddSwaggerAutenticacaoJwtBearer(); ; // Adicionando configuração JWT Tokens no swagger

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddConfigureLowerCaseRoutes();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUIDoc(app.Services); // Configura suporte a documentação

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
