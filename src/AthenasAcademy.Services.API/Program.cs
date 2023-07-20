using AthenasAcademy.Services.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicosScoped();// Adicionando Services

builder.Services.AddRepositoriosSingleton();// Adicionando Repositories

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documenta��o

builder.Services.AddAutenticacaoJwtBearer(builder.Configuration); // Adicionando configura��o JWT Tokens

builder.Services.AddSwaggerAutenticacaoJwtBearer(); ; // Adicionando configura��o JWT Tokens no swagger

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddConfigureLowerCaseRoutes();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUIDoc(app.Services); // Configura suporte a documenta��o

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
