using Scalar.AspNetCore;
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

builder.Services.AddEndpointsApiExplorer();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*");
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference(opt => opt
    .WithTitle("StrengthSync")
    .WithTheme(ScalarTheme.DeepSpace)
    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
