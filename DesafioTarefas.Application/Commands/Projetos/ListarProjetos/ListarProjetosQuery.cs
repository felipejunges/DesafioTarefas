using DesafioTarefas.Application.Models.Projetos;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.ListarProjetos
{
    public class ListarProjetosQuery : IRequest<IEnumerable<ProjetoResponse>>
    {
    }
}
