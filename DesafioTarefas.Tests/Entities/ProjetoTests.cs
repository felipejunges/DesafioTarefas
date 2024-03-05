using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;

namespace DesafioTarefas.Tests.Entities
{
    public class ProjetoTests
    {
        [Fact]
        public void DeveSerPossivelAdicionarTarefaEmProjetoDisponivel()
        {
            var tarefas = Enumerable.Repeat(new Tarefa(Guid.NewGuid(), Prioridade.Media), Projeto.LIMITE_TAREFAS - 1).ToList();
            var usuario = new Usuario();
            var projeto = new Projeto(Guid.NewGuid(), tarefas, usuario);

            var result = projeto.AdicionarTarefa(new Tarefa(Guid.NewGuid(), Prioridade.Media));

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelAdicionarTarefaEmProjetoLotado()
        {
            var tarefas = Enumerable.Repeat(new Tarefa(Guid.NewGuid(), Prioridade.Media), Projeto.LIMITE_TAREFAS).ToList();
            var usuario = new Usuario();
            var projeto = new Projeto(Guid.NewGuid(), tarefas, usuario);

            var result = projeto.AdicionarTarefa(new Tarefa(Guid.NewGuid(), Prioridade.Media));

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeSemTarefas()
        {
            var tarefas = new List<Tarefa>();
            var usuario = new Usuario();
            var projeto = new Projeto(Guid.NewGuid(), tarefas, usuario);

            var result = projeto.ValidarPodeSerRemovido();

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeSemTarefasPendentes()
        {
            var tarefas = new List<Tarefa>()
            {
                new Tarefa(Guid.NewGuid(), Prioridade.Media, Status.Concluida),
                new Tarefa(Guid.NewGuid(), Prioridade.Media, Status.Concluida)
            };

            var usuario = new Usuario();
            var projeto = new Projeto(Guid.NewGuid(), tarefas, usuario);

            var result = projeto.ValidarPodeSerRemovido();

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NaoDeveSerPossivelRemoverProjetoSeComTarefasPendentes()
        {
            var tarefas = new List<Tarefa>()
            {
                new Tarefa(Guid.NewGuid(), Prioridade.Media, Status.Concluida),
                new Tarefa(Guid.NewGuid(), Prioridade.Media, Status.Pendente)
            };

            var usuario = new Usuario();
            var projeto = new Projeto(Guid.NewGuid(), tarefas, usuario);

            var result = projeto.ValidarPodeSerRemovido();

            Assert.False(result.IsSuccess);
        }
    }
}
