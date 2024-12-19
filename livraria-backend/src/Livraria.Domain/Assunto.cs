using Livraria.Core.EntidadeBase;

namespace Livraria.Domain
{
    public class Assunto : EntidadeBase
    {
        public Assunto(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; private set; }

        public IEnumerable<LivroAssunto> LivrosAssuntos => _livrosAssuntos;
        private readonly List<LivroAssunto> _livrosAssuntos = [];
    }
}
