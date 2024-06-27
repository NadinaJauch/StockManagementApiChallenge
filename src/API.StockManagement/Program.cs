using API.StockManagement.Application;
using API.StockManagement.Extensions;
using API.StockManagement.Infrastructure;
using API.StockManagement.Infrastructure.Security;
using API.StockManagement.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtAuth();

builder.Services.AddJwtAuthentication(builder.Configuration);
// Configurar opciones de tokens
builder.Services.Configure<Token>(builder.Configuration.GetSection("JWT"));

builder.Services.AddApplicationModule();
builder.Services.AddInfrastructureModule(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

builder.Services.AddProblemDetails();

builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
