using Flunt.Notifications;
using Livraria.Application.Commands.AutorCommands.Contracts;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.AutorCommands
{
    public class AdicionarAutorCommand : Notifiable<Notification>, ICommand
    {
        public AdicionarAutorCommand(string nome)
        {
            Nome = nome;

            AddNotifications(new AdicionarAutorContract(this));
        }

        public string Nome { get; private set; }
    }
}
