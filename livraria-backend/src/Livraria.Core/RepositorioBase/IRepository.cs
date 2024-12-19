using Livraria.Core.EntidadeBase;

namespace Livraria.Core.RepositorioBase
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
