using Flunt.Validations;

namespace Livraria.Application.Commands.AutorCommands.Contracts
{
    public class AdicionarAutorContract : Contract<AdicionarAutorCommand>
    {
        public AdicionarAutorContract(AdicionarAutorCommand command) =>
            Requires()
                .IsNotNullOrWhiteSpace(command.Nome, "Nome", "Campo obrigatório.");
    }
}
