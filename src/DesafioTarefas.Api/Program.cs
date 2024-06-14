using DesafioTarefas.Api.Configuration;
using DesafioTarefas.Api.Endpoints;
using DesafioTarefas.Application;
using DesafioTarefas.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DesafioContext>(db =>
{
    db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly));

builder.Services.AddDesafioServices();
builder.Services.AddDesafioValidators();

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

app.EnsureDbCreated();

app.MapProjetosEndpoints();
app.MapTarefasEndpoints();
app.MapComentariosEndpoints();
app.MapRelatoriosEndpoints();

app.Run();
