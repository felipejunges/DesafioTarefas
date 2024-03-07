using DesafioTarefas.Domain;
using DesafioTarefas.Domain.Dtos;
using MediatR;

namespace DesafioTarefas.Application.Commands.Relatorios.MediaTarefasConcluidas
{
    public class MediaTarefasConcluidasQuery : IRequest<Result<RelatorioMediaTarefaConcluidaDto>>
    {
        public int QuantidadeDias { get; init; }
    }
}
