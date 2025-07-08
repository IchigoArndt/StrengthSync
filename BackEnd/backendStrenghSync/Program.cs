using Scalar.AspNetCore;
using Serilog;
using SimpleInjector;
using StrengthSync.Infra.Configurations;
using StrengthSync.Infra.Extensions;

try
{
    // Configurações de logs
    Log.Logger = new LoggerConfiguration()
                     .WriteTo.MongoDB(Environment.GetEnvironmentVariable("Connection_Mongo"), "Logs")
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();

    Log.Information("Iniciando Api StrenghSync ...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro fatal ao iniciar a api.");
}
finally
{
    Log.CloseAndFlush();
}


