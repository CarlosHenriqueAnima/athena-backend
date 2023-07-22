using AthenasAcademy.Services.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddServicosScoped();// Adicionando Services

builder.Services.AddRepositoriosSingleton();// Adicionando Repositories

builder.Services.AddApiVersionamento(); // Adicionando suporte a versionamento

builder.Services.AddApiVersionamentoExplorer(); // Adicionando suporte a versionamento explorer

builder.Services.AddSwaggerGenDoc("API Athenas Academy", "1.0");// Adicionando suporte a documenta��o

builder.Services.AddSwaggerAutenticacaoJwtBearer(); // Adicionando configura��o JWT Tokens no swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigureLowerCaseRoutes();

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
