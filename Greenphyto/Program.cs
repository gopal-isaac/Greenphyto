using GreenServices;
using Microsoft.FeatureManagement;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GreenServices.DependencyInjection).Assembly));


// Register services using the extension methods. Look inside GreenServices/DependencyInjection.cs
builder.Services.AddApplicationServices()
                .AddDatabase(builder.Configuration)
                .AddHttpClients();

// Feature Flag
// Ideally, the configs can sit in the database.
// Have an api for the flags so that it can interface with a frontend so that non technical people can enable/disable it.
// Questions: will this work at runtime?
// TODO: build a webapi project for featureflag 
// TODO: explore Feature filter
builder.Services.AddFeatureManagement(
    builder.Configuration.GetSection("FeatureFlags")
    );

// Add Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
