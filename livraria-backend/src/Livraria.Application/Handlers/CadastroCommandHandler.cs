using Livraria.Application.Commands.AutorCommands;
using Livraria.Application.Commands.LivroCommands;
using Livraria.Application.Repositories;
using Livraria.Core.Mensagens.Commands;
using Livraria.Domain;

namespace Livraria.Application.Handlers
{
    public class CadastroCommandHandler :
        ICommandHandler<AdicionarAutorCommand>,
        ICommandHandler<AlterarAutorCommand>,
        ICommandHandler<RemoverAutorCommand>,

        ICommandHandler<AdicionarLivroCommand>,
        ICommandHandler<AlterarLivroCommand>,
        ICommandHandler<RemoverLivroCommand>
    {
        private readonly ICadastroRepository _cadastroRepository;

        public CadastroCommandHandler(ICadastroRepository cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        public async Task<CommandResult> Handler(AdicionarAutorCommand command)
        {
            var autor = new Autor(command.Nome);

            await _cadastroRepository.AdicionarAutor(autor);
            var commit = await _cadastroRepository.UnitOfWork.Commit();

            return commit
                ? new CommandResult(true, "Autor foi adicionado com sucesso", autor.Id)
                : new CommandResult(false, "Ocorreu um erro ao adicionar Autor", command);
        }

        public async Task<CommandResult> Handler(AlterarAutorCommand command)
        {
            var autor = await _cadastroRepository.ObterAutorPorId(command.AutorId);
            if (autor is null) return new CommandResult(false, "Autor não encontrado", command);

            autor.Alterar(command.Nome);

            _cadastroRepository.AlterarAutor(autor);
            var commit = await _cadastroRepository.UnitOfWork.Commit();

            return commit
                ? new CommandResult(true, "Autor foi alterado com sucesso", autor.Id)
                : new CommandResult(false, "Ocorreu um erro ao alterar Autor", command);
        }

        public async Task<CommandResult> Handler(RemoverAutorCommand command)
        {
            var autor = await _cadastroRepository.ObterAutorPorId(command.AutorId);
            if (autor is null) return new CommandResult(false, "Autor não encontrado", command);

            _cadastroRepository.RemoverAutor(autor);
            var commit = await _cadastroRepository.UnitOfWork.Commit();

            return commit
                ? new CommandResult(true, "Autor foi removido com sucesso", autor.Id)
                : new CommandResult(false, "Ocorreu um erro ao remover Autor", command);
        }

        public async Task<CommandResult> Handler(AdicionarLivroCommand command)
        {
            await _cadastroRepository.UnitOfWork.BeginTransactionAsync();

            var autores = await _cadastroRepository.ObterAutoresPorIds(command.Autores);
            if (autores?.Count() != command.Autores.Count)
                return new CommandResult(false, "Alguns autores informados não existem", command);

            var assuntosExistentes = await _cadastroRepository.ObterAssuntosPorNomes(command.Assuntos);
            var novosAssuntos = command.Assuntos
                .Where(a => !assuntosExistentes.Any(ae => ae.Descricao.Equals(a, StringComparison.OrdinalIgnoreCase)))
                .Select(a => new Assunto(a))
                .ToList();

            foreach (var novoAssunto in novosAssuntos) 
                await _cadastroRepository.AdicionarAssunto(novoAssunto);

            await _cadastroRepository.UnitOfWork.Commit();

            var todosAssuntos = assuntosExistentes.Concat(novosAssuntos).ToList();

            var livro = new Livro(
                command.Titulo,
                command.Editora,
                command.Edicao,
                command.AnoPublicacao
            );

            foreach (var autorId in command.Autores) 
                livro.AdicionarAutor(livro.Id, autorId);

            foreach (var assunto in todosAssuntos) 
                livro.AdicionarAssunto(livro.Id, assunto.Id);

            foreach (var valorCompra in command.ValoresCompra)
                livro.AdicionarValorCompra(livro.Id, valorCompra.Tipo, valorCompra.Valor);

            await _cadastroRepository.AdicionarLivro(livro);
            var commit = await _cadastroRepository.UnitOfWork.CommitTransactionAsync();

            return commit
                ? new CommandResult(true, "Livro foi adicionado com sucesso", livro.Id)
                : new CommandResult(false, "Ocorreu um erro ao adicionar livro", command);
        }

        public async Task<CommandResult> Handler(AlterarLivroCommand command)
        {
            await _cadastroRepository.UnitOfWork.BeginTransactionAsync();

            var livro = await _cadastroRepository.ObterLivroPorId(command.LivroId);
            if (livro == null)
                return new CommandResult(false, "Livro não encontrado", command);

            var autores = await _cadastroRepository.ObterAutoresPorIds(command.Autores);
            if (autores?.Count() != command.Autores.Count)
                return new CommandResult(false, "Alguns autores informados não existem", command);

            var assuntosExistentes = await _cadastroRepository.ObterAssuntosPorNomes(command.Assuntos);
            var novosAssuntos = command.Assuntos
                .Where(a => !assuntosExistentes.Any(ae => ae.Descricao.Equals(a, StringComparison.OrdinalIgnoreCase)))
                .Select(a => new Assunto(a))
                .ToList();

            foreach (var novoAssunto in novosAssuntos)
                await _cadastroRepository.AdicionarAssunto(novoAssunto);

            await _cadastroRepository.UnitOfWork.Commit();

            var todosAssuntos = assuntosExistentes.Concat(novosAssuntos).ToList();

            livro.Alterar(
                command.Titulo, 
                command.Editora, 
                command.Edicao,
                command.AnoPublicacao);

            livro.RemoverAutores();
            foreach (var autorId in command.Autores)
                livro.AdicionarAutor(livro.Id, autorId);

            livro.RemoverAssuntos();
            foreach (var assunto in todosAssuntos)
                livro.AdicionarAssunto(livro.Id, assunto.Id);

            livro.RemoverValoresCompra();
            foreach (var valorCompra in command.ValoresCompra)
                livro.AdicionarValorCompra(livro.Id, valorCompra.Tipo, valorCompra.Valor);

            _cadastroRepository.AlterarLivro(livro);
            var commit = await _cadastroRepository.UnitOfWork.CommitTransactionAsync();

            return commit
                ? new CommandResult(true, "Livro foi alterado com sucesso", livro.Id)
                : new CommandResult(false, "Ocorreu um erro ao alterar o livro", command);
        }

        public async Task<CommandResult> Handler(RemoverLivroCommand command)
        {
            await _cadastroRepository.UnitOfWork.BeginTransactionAsync();

            var livro = await _cadastroRepository.ObterLivroPorId(command.LivroId);
            if (livro == null)
                return new CommandResult(false, "Livro não encontrado", command);

            _cadastroRepository.RemoverLivro(livro);

            var commit = await _cadastroRepository.UnitOfWork.CommitTransactionAsync();

            return commit
                ? new CommandResult(true, "Livro foi removido com sucesso", livro.Id)
                : new CommandResult(false, "Ocorreu um erro ao remover o livro", command);
        }
    }
}
