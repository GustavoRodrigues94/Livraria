namespace Livraria.Application.Queries.ViewModels
{
    public class AssuntoViewModel(int assuntoId, string descricao)
    {
        public int AssuntoId { get; private set; } = assuntoId;
        public string Descricao { get; private set; } = descricao;
    }
}
