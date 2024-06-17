using DesafioTarefas.Api.Services;
using DesafioTarefas.Application.Commands.Tarefas.AlterarTarefa;
using DesafioTarefas.Application.Commands.Tarefas.IncluirComentario;
using DesafioTarefas.Application.Commands.Tarefas.IncluirTarefa;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using DesafioTarefas.Infra.Contexts;
using DesafioTarefas.Infra.Repositories;
using DesafioTarefas.Infra.UnitsOfWork;
using FluentValidation;
using System.Data;

namespace DesafioTarefas.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDesafioServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserResolverService, UserResolverService>()
                .AddScoped<IProjetoRepository, ProjetoRepository>()
                .AddScoped<ITarefaRepository, TarefasRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddDesafioValidators(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<AlterarTarefaCommand>, AlterarTarefaCommandValidator>()
                .AddScoped<IValidator<IncluirComentarioCommand>, IncluirComentarioCommandValidator>()
                .AddScoped<IValidator<IncluirTarefaCommand>, IncluirTarefaCommandValidator>();

            return services;
        }

        public static IHost EnsureDbCreated(this IHost app)
        {
            int tentativa = 0;

            var logger = app.Services.GetService<ILogger<Program>>();

            while (tentativa < 10)
            {
                tentativa++;

                logger?.LogInformation("Aguardando {tempo} segundos para criar {contexto}", tentativa, nameof(DesafioContext));
                Thread.Sleep(tentativa * 1000);

                try
                {
                    var scope = app.Services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DesafioContext>();
                    db.Database.EnsureCreated();

                    logger?.LogInformation("Criada database {contexto}", nameof(DesafioContext));

                    return app;
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "Não foi possível conectar ao {contexto}. Tentativa {tentativa}.", nameof(DesafioContext), tentativa);
                }
            }

            throw new DataException("Não foi possível conectar no {nameof(DesafioContext)} depois de 10 tentativas");
        }
    }
}