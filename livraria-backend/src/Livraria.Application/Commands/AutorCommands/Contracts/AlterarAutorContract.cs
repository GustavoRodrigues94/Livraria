using Flunt.Validations;

namespace Livraria.Application.Commands.AutorCommands.Contracts
{
    public class AlterarAutorContract : Contract<AlterarAutorCommand>
    {
        public AlterarAutorContract(AlterarAutorCommand command) =>
            Requires()
                .IsNotNull(command.AutorId, "AutorId", "Campo obrigatório.")
                .IsNotNullOrWhiteSpace(command.Nome, "Nome", "Campo obrigatório.");
    }
}
