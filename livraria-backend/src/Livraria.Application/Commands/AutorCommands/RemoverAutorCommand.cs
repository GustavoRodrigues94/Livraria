using Flunt.Notifications;
using Livraria.Application.Commands.AutorCommands.Contracts;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.AutorCommands
{
    public class RemoverAutorCommand : Notifiable<Notification>, ICommand
    {
        public RemoverAutorCommand(int autorId)
        {
            AutorId = autorId;

            AddNotifications(new RemoverAutorContract(this));
        }

        public int AutorId { get; private set; }
    }
}
