using Livraria.Application.Queries.ViewModels;
using Livraria.Application.Services;
using Microsoft.Extensions.Hosting;
using RazorEngine;
using RazorEngine.Templating;

namespace Livraria.Infra.RelatorioService
{
    public class RelatorioService(IHostEnvironment environment) : IRelatorioService
    {
        public async Task<byte[]> GerarRelatorioLivrosAgrupadoAutorAsync(IEnumerable<RelatorioLivroViewModel> dadosRelatorio)
        {
            var templatePath = Path.Combine(environment.ContentRootPath, "Templates", "RelatorioLivros.cshtml");
            var templateContent = await File.ReadAllTextAsync(templatePath);

            var htmlContent = Engine.Razor.RunCompile(templateContent, "RelatorioLivros", typeof(List<RelatorioLivroViewModel>), dadosRelatorio);

            var pdfBytes = GerarPdf(htmlContent);

            return pdfBytes;
        }

        private static byte[] GerarPdf(string htmlContent)
        {
            var renderer = new ChromePdfRenderer();
            var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

            return pdfDocument.BinaryData;
        }
    }
}
