using System.Text.Json.Serialization;

namespace DesafioTarefas.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Prioridade
    {
        Baixa = 0,
        Media = 1,
        Alta = 2
    }
}