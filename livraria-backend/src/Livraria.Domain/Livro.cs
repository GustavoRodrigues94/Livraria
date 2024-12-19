using Livraria.Core.EntidadeBase;
using Livraria.Core.Enums;

namespace Livraria.Domain
{
    public class Livro : EntidadeBase, IAggregateRoot
    {
        public Livro(string titulo, string editora, int edicao, string anoPublicacao)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }

        public string Titulo { get; private set; }
        public string Editora { get; private set; }
        public int Edicao { get; private set; }
        public string AnoPublicacao { get; private set; }

        private readonly List<LivroAutor> _livrosAutores = [];
        public IEnumerable<LivroAutor> LivrosAutores => _livrosAutores;

        private readonly List<LivroAssunto> _livrosAssuntos = [];
        public IEnumerable<LivroAssunto> LivrosAssuntos => _livrosAssuntos;

        private readonly List<LivroValorCompra> _livroValoresCompra = [];
        public IEnumerable<LivroValorCompra> LivroValoresCompra => _livroValoresCompra;

        public void AdicionarAutor(int livroId, int autorId)
        {
            _livrosAutores.Add(new LivroAutor(livroId, autorId));
        }

        public void AdicionarAssunto(int livroId, int assuntoId)
        {
            _livrosAssuntos.Add(new LivroAssunto(livroId, assuntoId));
        }

        public void AdicionarValorCompra(int livroId, TipoDeCompraEnum tipo, decimal valor)
        {
            _livroValoresCompra.Add(new LivroValorCompra(
                livroId,
                tipo,
                valor
            ));
        }

        public void Alterar(string titulo, string editora, int edicao, string anoPublicacao)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }

        public void RemoverAutores()
        {
            _livrosAutores?.Clear();
        }

        public void RemoverAssuntos()
        {
            _livrosAssuntos?.Clear();
        }

        public void RemoverValoresCompra()
        {
            _livroValoresCompra?.Clear();
        }
    }
}
