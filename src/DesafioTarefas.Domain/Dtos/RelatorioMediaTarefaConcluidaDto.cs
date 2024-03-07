namespace DesafioTarefas.Domain.Dtos
{
    public record RelatorioMediaTarefaConcluidaDto(IEnumerable<RelatorioMediaTarefaConcluidaItemDto> Itens);

    public record RelatorioMediaTarefaConcluidaItemDto(Guid UsuarioId, int QuantidadeTarefasConcluidas);
}