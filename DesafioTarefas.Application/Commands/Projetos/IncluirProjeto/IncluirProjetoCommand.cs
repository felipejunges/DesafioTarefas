using DesafioTarefas.Application.Models.Projetos;
using DesafioTarefas.Domain;
using MediatR;

namespace DesafioTarefas.Application.Commands.Projetos.IncluirProjeto
{
    public class IncluirProjetoCommand : IRequest<Result<ProjetoResponse>>
    {
    }
}
