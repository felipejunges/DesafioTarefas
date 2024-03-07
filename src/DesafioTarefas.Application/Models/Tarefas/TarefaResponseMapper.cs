using DesafioTarefas.Domain.Entities;

namespace DesafioTarefas.Application.Models.Tarefas
{
    public static class TarefaResponseMapper
    {
        public static IEnumerable<TarefaResponse> Map(IEnumerable<Tarefa> tarefas)
        {
            return tarefas.Select(Map);
        }

        public static TarefaResponse Map(Tarefa tarefa)
        {
            return new TarefaResponse(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Prioridade,
                tarefa.Status,
                tarefa.DataPrazo,
                tarefa.DataConclusao,
                tarefa.Observacoes);
        }
    }
}
