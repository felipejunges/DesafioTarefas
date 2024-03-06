using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Application.Models.Tarefas
{
    public record TarefaResponse(
        Guid Id,
        string Titulo,
        Prioridade Prioridade,
        Status Status,
        DateOnly DataPrazo,
        string? Observacoes);
}
