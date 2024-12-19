namespace Livraria.Application.Queries.ViewModels
{
    public class AutorViewModel(int autorId, string nome)
    {
        public int AutorId { get; private set; } = autorId;
        public string Nome { get; private set; } = nome;
    }
}
