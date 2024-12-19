namespace Livraria.Core.Mensagens.Commands
{
    public class CommandResult(bool sucesso, string mensagem, object dado) : ICommandResult
    {
        public bool Sucesso { get; set; } = sucesso;
        public string Mensagem { get; set; } = mensagem;
        public object Dado { get; set; } = dado;
    }
}
