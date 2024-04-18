using System.Text.Json.Serialization;

namespace DesafioTarefas.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Pendente = 0,
        EmAndamento = 1,
        Concluida = 2
    }
}
