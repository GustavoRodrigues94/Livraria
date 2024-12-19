using Livraria.Application.Commands.RelatorioCommands;
using Livraria.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [ApiController]
    [Route("v1/relatorio")]
    public class RelatorioController : ControllerBase
    {
        private readonly RelatorioCommandHandler _relatorioCommandHandler;
        public RelatorioController([FromServices] RelatorioCommandHandler relatorioCommandHandler)
        {
            _relatorioCommandHandler = relatorioCommandHandler;
        }

        [HttpGet]
        [Route("livros-agrupado-autores")]
        public async Task<IActionResult> GerarRelatorioLivrosAgrupadoPorAutor()
        {
            var result = await _relatorioCommandHandler.Handler(new GerarRelatorioLivrosAgrupadoAutor());

            if (!result.Sucesso || result.Dado is not byte[] pdfBytes)
                return BadRequest(result.Mensagem);

            return File(pdfBytes, "application/pdf", "RelatorioLivros.pdf");
        }
    }
}
