using DesafioTarefas.Domain.Entities;

namespace DesafioTarefas.Application.Models.Projetos
{
    public static class ProjetoResponseMapper
    {
        public static IEnumerable<ProjetoResponse> Map(IEnumerable<Projeto> projetos)
        {
            return projetos.Select(Map);
        }

        public static ProjetoResponse Map(Projeto projeto)
        {
            return new ProjetoResponse(
                projeto.Id,
                projeto.DataHoraCadastro);
        }
    }
}
