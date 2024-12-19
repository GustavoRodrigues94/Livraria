using Flunt.Validations;

namespace Livraria.Application.Commands.AutorCommands.Contracts
{
    public class RemoverAutorContract : Contract<RemoverAutorCommand>
    {
        public RemoverAutorContract(RemoverAutorCommand command) =>
            Requires()
                .IsNotNull(command.AutorId, "AutorId", "Campo obrigatório.");
    }
}
