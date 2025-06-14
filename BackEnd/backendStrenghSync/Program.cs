using SimpleInjector;
using StrengthSync.Infra.Configurations;
using StrengthSync.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurações
EnviromentConfiguration.Configure(builder.Configuration);

// Criação do container
var container = new Container();

// Registrar serviços padrão
builder.Services.AddDefaultServices<Program>(container);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
