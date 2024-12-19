using Flunt.Notifications;
using Livraria.Application.Commands.AutorCommands.Contracts;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.AutorCommands
{
    public class AlterarAutorCommand : Notifiable<Notification>, ICommand
    {
        public AlterarAutorCommand(int autorId, string nome)
        {
            AutorId = autorId;
            Nome = nome;

            AddNotifications(new AlterarAutorContract(this));
        }

        public int AutorId { get; private set; }
        public string Nome { get; private set; }
    }
}
