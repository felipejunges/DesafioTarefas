using DesafioTarefas.Api.Services;
using DesafioTarefas.Application;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using DesafioTarefas.Infra.Contexts;
using DesafioTarefas.Infra.Repositories;
using DesafioTarefas.Infra.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DesafioContext>(db =>
{
    db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));

builder.Services.AddScoped<IUserResolverService, UserResolverService>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefasRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DesafioContext>();
db.Database.EnsureCreated();

app.Run();
