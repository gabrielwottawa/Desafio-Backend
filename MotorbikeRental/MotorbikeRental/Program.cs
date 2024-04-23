using MotorbikeRental.Configurations;
using MotorbikeRental.Domain;

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

builder.Services.AddHttpContextAccessor();
builder.Services.AddLocalization();
builder.Services.AddControllers();

builder.Services.AddDomainBootstrapper();
builder.AddMediatRConfiguration();
builder.AddSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();

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
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();