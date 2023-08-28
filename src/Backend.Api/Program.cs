using Backend.Api.Configurations;
using Backend.Api.Middlewares;
using Backend.Infra.Configurations;
using Backend.Ioc.Injectors;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

Console.WriteLine(builder.Environment.EnvironmentName);

// Add serilog configurations
SerilogSetup.ConfigureSerilog(builder.Configuration);

builder.Host.UsingSerilog();

// Add services to the container.

builder.Services
    .AddingCors()
    .AddingResponseCompression()
    .AddingAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddingSwagger();

builder.Services
    .AddDbContextInjector(builder.Configuration)
    .AddProjectInjectors();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMiddleware<ExceptionInDevelopmentEnvironmentMiddleware>();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.MigrateDatabase();

app.UsingSwagger();

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x =>
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();


app.UsingSerilogRequestLogging();

app.MapControllers();

app.Run();
