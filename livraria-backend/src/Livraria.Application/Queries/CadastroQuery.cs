using Livraria.Application.Queries.ViewModels;
using Livraria.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Application.Queries
{
    public class CadastroQuery : ICadastroQuery
    {
        private readonly ICadastroRepository _cadastroRepository;

        public CadastroQuery(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        public async Task<AutorViewModel?> ObterAutorPorId(int autorId)
        {
            var autor = await _cadastroRepository.ObterAutorPorId(autorId);

            return autor is null
                ? null
                : new AutorViewModel(
                    autor.Id, 
                    autor.Nome);
        }

        public async Task<IEnumerable<AutorViewModel>> ObterAutores() =>
            await _cadastroRepository.ObterAutorQueryable()
                .Select(autor => new AutorViewModel(
                    autor.Id,
                    autor.Nome))
                .ToListAsync();

        public async Task<LivroViewModel?> ObterLivroPorId(int livroId)
        {
            var livro = await _cadastroRepository.ObterLivroPorId(livroId);
            if (livro is null) return null;

            return new LivroViewModel(
                livro.Id,
                livro.Titulo,
                livro.Editora,
                livro.Edicao,
                livro.AnoPublicacao,
                livro.LivrosAutores.Select(la => new AutorViewModel(la.AutorId, la.Autor.Nome)),
                livro.LivroValoresCompra.Select(vc => new ValorCompraViewModel(vc.Tipo, vc.Valor)),
                livro.LivrosAssuntos.Select(la => new AssuntoViewModel(la.AssuntoId, la.Assunto.Descricao))
            );
        }

        public async Task<IEnumerable<LivroViewModel>> ObterLivros() =>
            await _cadastroRepository.ObterLivroQueryable()
                .Select(livro => new LivroViewModel(
                    livro.Id,
                    livro.Titulo,
                    livro.Editora,
                    livro.Edicao,
                    livro.AnoPublicacao,
                    livro.LivrosAutores.Select(la => new AutorViewModel(la.AutorId, la.Autor.Nome)),
                    livro.LivroValoresCompra.Select(vc => new ValorCompraViewModel(vc.Tipo, vc.Valor)),
                    livro.LivrosAssuntos.Select(la => new AssuntoViewModel(la.AssuntoId, la.Assunto.Descricao))))
                .ToListAsync();
    }
}
