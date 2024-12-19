using Flunt.Validations;

namespace Livraria.Application.Commands.LivroCommands.Contracts
{
    public class AdicionarLivroContract : Contract<AdicionarLivroCommand>
    {
        public AdicionarLivroContract(AdicionarLivroCommand command)
        {
            Requires()
                .IsNotNullOrWhiteSpace(command.Titulo, "Titulo", "O título do livro é obrigatório.")
                .IsNotNull(command.Autores, "Autores", "Os autores devem ser informados.")
                .IsGreaterThan(command.Autores.Count, 0, "Autores", "Deve haver ao menos um autor.")
                .IsNotNullOrWhiteSpace(command.Editora, "Editora", "A editora é obrigatória.")
                .IsNotNull(command.Edicao, "Edicao", "A Edição deve ser informada.")
                .IsNotNullOrWhiteSpace(command.AnoPublicacao, "AnoPublicacao", "O ano de publicação é obrigatório.")
                .AreEquals(command.AnoPublicacao, 4, "AnoPublicacao", "O ano de publicação deve ter 4 caracteres.")
                .IsNotNull(command.ValoresCompra, "ValoresCompra", "Os valores de compra são obrigatórios.")
                .IsGreaterThan(command.ValoresCompra.Count, 0, "ValoresCompra", "Deve haver ao menos um valor de compra.");

            foreach (var valor in command.ValoresCompra)
                Requires()
                    .IsGreaterThan(valor.Valor, 0, "Valor", "O valor deve ser maior que zero.");
        }
    }
}
