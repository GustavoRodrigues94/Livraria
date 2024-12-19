using Flunt.Notifications;
using Livraria.Application.Commands.LivroCommands.Contracts;
using Livraria.Application.Commands.LivroCommands.DTOsCommands;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.LivroCommands
{
    public class AdicionarLivroCommand : Notifiable<Notification>, ICommand
    {
        public AdicionarLivroCommand(
            string titulo,
            List<int> autores,
            string editora,
            int edicao,
            string anoPublicacao,
            List<ValorCompraDTO> valoresCompra,
            List<string> assuntos)
        {
            Titulo = titulo;
            Autores = autores;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
            ValoresCompra = valoresCompra;
            Assuntos = assuntos;

            AddNotifications(new AdicionarLivroContract(this));
        }

        public string Titulo { get; private set; }
        public List<int> Autores { get; private set; }
        public string Editora { get; private set; }
        public int Edicao { get; private set; }
        public string AnoPublicacao { get; private set; }
        public List<ValorCompraDTO> ValoresCompra { get; private set; }
        public List<string> Assuntos { get; private set; }
    }
}
