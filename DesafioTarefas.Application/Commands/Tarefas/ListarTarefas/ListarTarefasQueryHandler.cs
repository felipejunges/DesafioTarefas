using DesafioTarefas.Application.Models.Tarefas;
using DesafioTarefas.Domain.Repositories;
using MediatR;

namespace DesafioTarefas.Application.Commands.Tarefas.ListarTarefas
{
    public class ListarTarefasQueryHandler : IRequestHandler<ListarTarefasQuery, IEnumerable<TarefaResponse>>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public ListarTarefasQueryHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<IEnumerable<TarefaResponse>> Handle(ListarTarefasQuery query, CancellationToken cancellationToken)
        {
            var tarefas = await _tarefaRepository.ListarTarefasDoProjeto(query.ProjetoId);

            return TarefaResponseMapper.Map(tarefas);
        }
    }
}
