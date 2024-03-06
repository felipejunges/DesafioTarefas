using DesafioTarefas.Domain.Entities;
using DesafioTarefas.Domain.Enums;
using DesafioTarefas.Domain.ValueObject;

namespace DesafioTarefas.Tests.Domain.Entities
{
    public class TarefaTests
    {
        [Fact]
        public void NaoDeveGerarHistoricoSeCamposNaoForemEditados()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, tarefa.Status, tarefa.DataPrazo, default);

            Assert.False(tarefa.Historico.Any());
        }

        [Fact]
        public void DevemSerGeradosMaisHistoricosSeTodosCamposAlterados()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, "Novo Título", Status.EmAndamento, tarefa.DataPrazo.AddDays(1), default);

            var camposAlterados = 3;

            Assert.Equal(camposAlterados, tarefa.Historico.Count);
        }

        [Fact]
        public void DeveSerGeradoUmHistoricoAoModificarTitulo()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, "Novo Título", tarefa.Status, tarefa.DataPrazo, default);

            Assert.Single(tarefa.Historico);
            Assert.Equal(nameof(tarefa.Titulo), tarefa.Historico.First().Campo);
        }

        [Fact]
        public void DeveSerGeradoUmHistoricoAoModificarStatus()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.EmAndamento, tarefa.DataPrazo, default);

            Assert.Single(tarefa.Historico);
            Assert.Equal(nameof(tarefa.Status), tarefa.Historico.First().Campo);
        }

        [Fact]
        public void DeveSerGeradoUmHistoricoAoModificarDataPrazo()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, tarefa.Status, tarefa.DataPrazo.AddDays(1), default);

            Assert.Single(tarefa.Historico);
            Assert.Equal(nameof(tarefa.DataPrazo), tarefa.Historico.First().Campo);
        }

        [Fact]
        public void DeveSerGeradoUmHistoricoAoAdicionarUmComentario()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.AdicionarComentario(usuario.Id, "Este é um belo comentário!");

            Assert.Single(tarefa.Historico);
        }

        [Fact]
        public void TarefaInicialmenteNaoDeveTerDataConclusao()
        {
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            Assert.Null(tarefa.DataConclusao);
        }

        [Fact]
        public void NaoDeveHaverDataConclusaoEnquantoTarefaEmAndamento()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.EmAndamento, tarefa.DataPrazo, tarefa.Observacoes);

            Assert.Null(tarefa.DataConclusao);
        }

        [Fact]
        public void DeveSetarDataConclusaoAoConcluirATarefa()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.EmAndamento, tarefa.DataPrazo, tarefa.Observacoes);
            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.Concluida, tarefa.DataPrazo, tarefa.Observacoes);

            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), tarefa.DataConclusao);
        }

        [Fact]
        public void DeveLimparDataConclusaoAoReabrirATarefa()
        {
            var usuario = new Usuario(Guid.NewGuid(), "Felipe J.");
            var tarefa = Tarefa.CriarTarefaParaTeste(Status.Pendente);

            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.EmAndamento, tarefa.DataPrazo, tarefa.Observacoes);
            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.Concluida, tarefa.DataPrazo, tarefa.Observacoes);
            tarefa.Atualizar(usuario.Id, tarefa.Titulo, Status.EmAndamento, tarefa.DataPrazo, tarefa.Observacoes);

            Assert.Null(tarefa.DataConclusao);
        }
    }
}
