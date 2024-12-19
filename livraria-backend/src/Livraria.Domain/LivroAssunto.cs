using Livraria.Core.EntidadeBase;

namespace Livraria.Domain
{
    public class LivroAssunto : EntidadeBase
    {
        public LivroAssunto(int livroId, int assuntoId)
        {
            LivroId = livroId;
            AssuntoId = assuntoId;
        }

        public int LivroId { get; private set; }
        public Livro Livro { get; private set; } = null!;

        public int AssuntoId { get; private set; }
        public Assunto Assunto { get; private set; } = null!;
    }
}
