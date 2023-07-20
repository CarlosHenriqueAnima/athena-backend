using AthenasAcademy.Services.API.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adicionando Services
builder.Services.AddServicosScopo();

// Adicionando suporte a versionamento
builder.Services.AddApiVersionamento();
builder.Services.AddVersionamentoApiExplorer();

// Adicionando suporte a documentação
builder.Services.AddSwaggerGenDoc();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();
app.UseSwagger();

// Configura suporte a documentação
app.UseSwaggerUIDoc(app.Services);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
