using Livraria.Core.EntidadeBase;

namespace Livraria.Domain
{
    public class LivroAutor : EntidadeBase
    {
        public LivroAutor(int livroId, int autorId)
        {
            LivroId = livroId;
            AutorId = autorId;
        }

        public Livro Livro { get; private set; } = null!;
        public int LivroId { get; private set; }

        public Autor Autor { get; private set; } = null!;
        public int AutorId { get; private set; }
    }
}
