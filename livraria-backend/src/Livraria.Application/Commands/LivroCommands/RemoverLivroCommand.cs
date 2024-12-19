using Flunt.Notifications;
using Livraria.Application.Commands.LivroCommands.Contracts;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.LivroCommands
{
    public class RemoverLivroCommand : Notifiable<Notification>, ICommand
    {
        public RemoverLivroCommand(int livroId)
        {
            LivroId = livroId;

            AddNotifications(new RemoverLivroContract(this));
        }

        public int LivroId { get; private set; }
    }
}
