using Livraria.Application.Queries.ViewModels;

namespace Livraria.Application.Queries
{
    public interface ICadastroQuery
    {
        Task<AutorViewModel?> ObterAutorPorId(int autorId);
        Task<IEnumerable<AutorViewModel>> ObterAutores();

        Task<LivroViewModel?> ObterLivroPorId(int livroId);
        Task<IEnumerable<LivroViewModel>> ObterLivros();
    }
}
