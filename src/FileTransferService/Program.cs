using System.Reflection;

using FileTransferService.Extensions;
using FileTransferService.Services.Implementations;
using FileTransferService.Services.Interfaces;

using Microsoft.OpenApi.Models;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Service for sending large files",
        Description = "ASP.NET Core Web API для проверки работы сервиса."
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom
                                                                 .Configuration(context.Configuration));

builder.AddOptions();
builder.Services.AddTransient<IFileService, FileService>();
builder.AddHttpClient();

var app = builder.Build();

app.AddAppMiddlewares();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();