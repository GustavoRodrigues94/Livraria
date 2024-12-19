using Livraria.Core.Enums;

namespace Livraria.Application.Commands.LivroCommands.DTOsCommands
{
    public class ValorCompraDTO(TipoDeCompraEnum tipo, decimal valor)
    {
        public TipoDeCompraEnum Tipo { get; private set; } = tipo;
        public decimal Valor { get; private set; } = valor;
    }
}
