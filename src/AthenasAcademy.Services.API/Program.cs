using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adicionando Services
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICertificadoService, CertificadoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IInscricaoService, InscricaoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
