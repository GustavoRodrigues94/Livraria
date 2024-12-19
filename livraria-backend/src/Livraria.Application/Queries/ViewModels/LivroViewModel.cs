namespace Livraria.Application.Queries.ViewModels
{
    public class LivroViewModel(
        int livroId,
        string titulo,
        string editora,
        int edicao,
        string anoPublicacao,
        IEnumerable<AutorViewModel> autores,
        IEnumerable<ValorCompraViewModel> valoresCompra,
        IEnumerable<AssuntoViewModel> assuntos)
    {
        public int LivroId { get; private set; } = livroId;
        public string Titulo { get; private set; } = titulo;
        public string Editora { get; private set; } = editora;
        public int Edicao { get; private set; } = edicao;
        public string AnoPublicacao { get; private set; } = anoPublicacao;
        public IEnumerable<AutorViewModel> Autores { get; private set; } = autores;
        public IEnumerable<ValorCompraViewModel> ValoresCompra { get; private set; } = valoresCompra;
        public IEnumerable<AssuntoViewModel> Assuntos { get; private set; } = assuntos;
    }
}
