using Livraria.Core.Enums;

namespace Livraria.Application.Queries.ViewModels
{
    public class ValorCompraViewModel(TipoDeCompraEnum tipo, decimal valor)
    {
        public TipoDeCompraEnum Tipo { get; private set; } = tipo;
        public decimal Valor { get; private set; } = valor;
    }
}
