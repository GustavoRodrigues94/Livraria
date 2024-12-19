namespace Livraria.Core.RepositorioBase
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task BeginTransactionAsync();
        Task<bool> CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
