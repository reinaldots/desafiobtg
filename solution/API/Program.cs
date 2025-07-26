using DesafioBTG.API.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Logging configurado automaticamente com o host builder
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Registro de serviços
builder.Services.AddSingleton<PedidoService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Log de ambiente
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("API iniciada no ambiente: {Environment}", app.Environment.EnvironmentName);

// Middleware global de tratamento de exceções
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var errorResponse = new
            {
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Detalhes = app.Environment.IsDevelopment() ? exception?.Message : null
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
