using Livraria.Application.Queries.ViewModels;

namespace Livraria.Application.Services
{
    public interface IRelatorioService
    {
        Task<byte[]> GerarRelatorioLivrosAgrupadoAutorAsync(IEnumerable<RelatorioLivroViewModel> dadosRelatorio);
    }
}
