namespace Livraria.Core.EntidadeBase
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase()
        {
        }

        public int Id { get; private set; }
    }
}