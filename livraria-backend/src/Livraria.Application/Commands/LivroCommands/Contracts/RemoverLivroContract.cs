using Flunt.Validations;

namespace Livraria.Application.Commands.LivroCommands.Contracts
{
    public class RemoverLivroContract : Contract<RemoverLivroCommand>
    {
        public RemoverLivroContract(RemoverLivroCommand command) =>
            Requires()
                .IsGreaterThan(command.LivroId, 0, "Id", "O ID do livro é obrigatório e deve ser maior que zero.");
    }
}
