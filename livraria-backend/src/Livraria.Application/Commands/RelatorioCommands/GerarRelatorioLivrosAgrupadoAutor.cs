using Flunt.Notifications;
using Livraria.Application.Commands.LivroCommands.Contracts;
using Livraria.Application.Commands.LivroCommands.DTOsCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Commands.RelatorioCommands
{
    public class GerarRelatorioLivrosAgrupadoAutor : Notifiable<Notification>, ICommand
    {
        public GerarRelatorioLivrosAgrupadoAutor()
        {
        }
    }
}
