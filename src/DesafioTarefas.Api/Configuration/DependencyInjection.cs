using DesafioTarefas.Api.Services;
using DesafioTarefas.Domain.Repositories;
using DesafioTarefas.Domain.Services;
using DesafioTarefas.Domain.UnitsOfWork;
using DesafioTarefas.Infra.Contexts;
using DesafioTarefas.Infra.Repositories;
using DesafioTarefas.Infra.UnitsOfWork;
using System.Data;

namespace DesafioTarefas.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDesafioServices(this IServiceCollection services)
        {
            services.AddScoped<IUserResolverService, UserResolverService>();
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<ITarefaRepository, TarefasRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IHost EnsureDbCreated(this IHost app)
        {
            int tentativa = 0;

            var logger = app.Services.GetService<ILogger<Program>>();

            while (tentativa < 10)
            {
                tentativa++;

                logger?.LogInformation($"Aguardando {{tempo}} segundos para criar {nameof(DesafioContext)}", tentativa);
                Thread.Sleep(tentativa * 1000);

                try
                {
                    var scope = app.Services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DesafioContext>();
                    db.Database.EnsureCreated();

                    logger?.LogInformation($"Criada database {nameof(DesafioContext)}");

                    return app;
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, $"Não foi possível conectar ao {nameof(DesafioContext)}. Tentativa {tentativa}.", tentativa);
                }
            }

            throw new DataException($"Não foi possível conectar no {nameof(DesafioContext)} depois de 10 tentativas");
        }
    }
}