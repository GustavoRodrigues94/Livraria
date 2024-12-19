using Livraria.Core.RepositorioBase;
using Livraria.Application.Repositories;
using Livraria.Domain;
using Livraria.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Repositories
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly LivrariaDbContext _contexto;
        public IUnitOfWork UnitOfWork => _contexto;

        public CadastroRepository(LivrariaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAutor(Autor autor)
        {
            await _contexto.Autor.AddAsync(autor);
        }

        public void AlterarAutor(Autor autor)
        {
            _contexto.Autor.Update(autor);
        }

        public void RemoverAutor(Autor autor)
        {
            _contexto.Autor.Remove(autor);
        }

        public async Task AdicionarAssunto(Assunto assunto)
        {
            await _contexto.Assunto.AddAsync(assunto);
        }

        public async Task AdicionarLivro(Livro livro)
        {
            await _contexto.Livro.AddAsync(livro);
        }

        public void AlterarLivro(Livro livro)
        {
            _contexto.Livro.Update(livro);
        }

        public void RemoverLivro(Livro livro)
        {
            _contexto.Livro.Remove(livro);
        }

        public async Task<Autor?> ObterAutorPorId(int autorId) =>
            await _contexto.Autor.FirstOrDefaultAsync(a => a.Id == autorId);

        public IQueryable<Autor> ObterAutorQueryable() =>
            _contexto.Autor
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(a => a.Nome);

        public async Task<IEnumerable<Autor>> ObterAutoresPorIds(List<int> autoresIds) =>
            await _contexto.Autor
                .Where(a => autoresIds.Contains(a.Id))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

        public async Task<IEnumerable<Assunto>> ObterAssuntosPorNomes(List<string> assuntos) =>
            await _contexto.Assunto
                .Where(a => assuntos.Contains(a.Descricao))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

        public async Task<Livro?> ObterLivroPorId(int livroId) =>
            await _contexto.Livro
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .Include(l => l.LivrosAssuntos)
                .ThenInclude(la => la.Assunto)
                .Include(l => l.LivroValoresCompra)
                .FirstOrDefaultAsync(l => l.Id == livroId);

        public IQueryable<Livro> ObterLivroQueryable() =>
            _contexto.Livro
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .Include(l => l.LivrosAssuntos)
                .ThenInclude(la => la.Assunto)
                .Include(l => l.LivroValoresCompra)
                .AsNoTrackingWithIdentityResolution();

        public void Dispose() => _contexto?.Dispose();
    }
}
