using Livraria.Core.EntidadeBase;

namespace Livraria.Domain
{
    public class Autor : EntidadeBase
    {
        public Autor(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }

        private readonly List<LivroAutor> _livrosAutores = [];
        public IEnumerable<LivroAutor> LivrosAutores => _livrosAutores;

        public void Alterar(string nome)
        {
            Nome = nome;
        }
    }
}
