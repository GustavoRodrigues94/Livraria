using Livraria.Core.RepositorioBase;
using Livraria.Domain;

namespace Livraria.Application.Repositories
{
    public interface ICadastroRepository : IRepository<Livro>
    {
        Task AdicionarAutor(Autor autor);
        void AlterarAutor(Autor autor);
        void RemoverAutor(Autor autor);

        Task AdicionarAssunto(Assunto assunto);

        Task AdicionarLivro(Livro livro);
        void AlterarLivro(Livro livro);
        void RemoverLivro(Livro livro);

        Task<Autor?> ObterAutorPorId(int autorId);
        IQueryable<Autor> ObterAutorQueryable();
        Task<IEnumerable<Autor>> ObterAutoresPorIds(List<int> autoresIds);

        Task<IEnumerable<Assunto>> ObterAssuntosPorNomes(List<string> assuntos);

        Task<Livro?> ObterLivroPorId(int livroId);
        IQueryable<Livro> ObterLivroQueryable();
    }
}
