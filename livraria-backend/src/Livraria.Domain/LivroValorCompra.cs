using Livraria.Core.EntidadeBase;
using Livraria.Core.Enums;

namespace Livraria.Domain
{
    public class LivroValorCompra : EntidadeBase
    {
        public LivroValorCompra(int livroId, TipoDeCompraEnum tipo, decimal valor)
        {
            LivroId = livroId;
            Tipo = tipo;
            Valor = valor;

            if (valor <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");
        }

        public int LivroId { get; private set; }
        public Livro Livro { get; private set; } = null!;

        public TipoDeCompraEnum Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
