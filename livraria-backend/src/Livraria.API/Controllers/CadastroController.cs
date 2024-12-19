using Livraria.Application.Commands.AutorCommands;
using Livraria.Application.Commands.LivroCommands;
using Livraria.Application.Handlers;
using Livraria.Application.Queries;
using Livraria.Application.Queries.ViewModels;
using Livraria.Core.Mensagens.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    [ApiController]
    [Route("v1/cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly CadastroCommandHandler _cadastroCommandHandler;
        private readonly ICadastroQuery _cadastroQuery;

        public CadastroController(
            [FromServices] CadastroCommandHandler cadastroCommandHandler,
            ICadastroQuery cadastroQuery)
        {
            _cadastroCommandHandler = cadastroCommandHandler;
            _cadastroQuery = cadastroQuery;
        }

        [HttpPost]
        [Route("autor")]
        public async Task<ActionResult<CommandResult>> AdicionarAutor(
            [FromBody] AdicionarAutorCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut]
        [Route("autor")]
        public async Task<ActionResult<CommandResult>> AlterarAutor(
            [FromBody] AlterarAutorCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete]
        [Route("autor")]
        public async Task<ActionResult<CommandResult>> RemoverAutor(
            [FromBody] RemoverAutorCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet]
        [Route("autor/{autorId:int}")]
        public async Task<AutorViewModel?> ObterAutorPorId(int autorId) =>
            await _cadastroQuery.ObterAutorPorId(autorId);

        [HttpGet]
        [Route("autores")]
        public async Task<IEnumerable<AutorViewModel>> ObterAutores() =>
            await _cadastroQuery.ObterAutores();

        [HttpPost]
        [Route("livro")]
        public async Task<ActionResult<CommandResult>> AdicionarLivro(
            [FromBody] AdicionarLivroCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut]
        [Route("livro")]
        public async Task<ActionResult<CommandResult>> AlterarLivro(
            [FromBody] AlterarLivroCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete]
        [Route("livro")]
        public async Task<ActionResult<CommandResult>> RemoverLivro(
            [FromBody] RemoverLivroCommand command)
        {
            if (command.IsValid is false) return BadRequest(command);

            var resultado = await _cadastroCommandHandler.Handler(command);

            if (resultado.Sucesso is false)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet]
        [Route("livro/{livroId:int}")]
        public async Task<LivroViewModel?> ObterLivroPorId(int livroId) =>
            await _cadastroQuery.ObterLivroPorId(livroId);

        [HttpGet]
        [Route("livros")]
        public async Task<IEnumerable<LivroViewModel>> ObterLivros() => 
            await _cadastroQuery.ObterLivros();
    }
}
