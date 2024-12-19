using Livraria.Application.Commands.RelatorioCommands;
using Livraria.Application.Queries.ViewModels;
using Livraria.Application.Repositories;
using Livraria.Application.Services;
using Livraria.Core.Mensagens.Commands;

namespace Livraria.Application.Handlers
{
    public class RelatorioCommandHandler :
        ICommandHandler<GerarRelatorioLivrosAgrupadoAutor>
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly IRelatorioService _relatorioService;

        public RelatorioCommandHandler(
            ICadastroRepository cadastroRepository,
            IRelatorioService relatorioService)
        {
            _cadastroRepository = cadastroRepository;
            _relatorioService = relatorioService;
        }

        public async Task<CommandResult> Handler(GerarRelatorioLivrosAgrupadoAutor command)
        {
            var livros = _cadastroRepository.ObterLivroQueryable();

            var dadosRelatorio = livros
                .SelectMany(livro => livro.LivrosAutores
                    .Select(la => new RelatorioLivroViewModel
                    {
                        Autor = la.Autor.Nome,
                        Livro = livro.Titulo,
                        Editora = livro.Editora,
                        AnoPublicacao = livro.AnoPublicacao,
                        Assuntos = string.Join(", ", livro.LivrosAssuntos.Select(las => las.Assunto.Descricao))
                    }))
                .OrderBy(vm => vm.Autor)
                .ThenBy(vm => vm.Livro)
                .ToList();

            var pdfBytes = await _relatorioService.GerarRelatorioLivrosAgrupadoAutorAsync(dadosRelatorio);

            return new CommandResult(true, "Relatório gerado com sucesso", pdfBytes);
        }
    }
}
