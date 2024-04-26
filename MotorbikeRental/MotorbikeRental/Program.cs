using MotorbikeRental.Configurations;
using MotorbikeRental.Domain;
using MotorbikeRental.Infrastructure;
using MotorbikeRental.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Host.ConfigureAppConfiguration((env, config) =>
{
    config
    .SetBasePath(env.HostingEnvironment.ContentRootPath)
    .AddJsonFile($"appsettings.{env.HostingEnvironment.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true)
    .AddEnvironmentVariables();
});

builder.AddJwtBearerConfiguration(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddLocalization();
builder.Services.AddControllers();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.MaxDepth = 64;
    });

builder.Services.AddDomainBootstrapper();
builder.Services.AddInfrastructureBootstrapper();
builder.Services.AddServicesBootstrapper();
builder.Services.AddEndpointsApiExplorer();

builder.AddMediatRConfiguration();
builder.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Motorbike Rental v1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();