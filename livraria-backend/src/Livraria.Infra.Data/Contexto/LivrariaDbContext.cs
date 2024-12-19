using Livraria.Core.RepositorioBase;
using Livraria.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Livraria.Infra.Data.Contexto
{
    public class LivrariaDbContext(DbContextOptions<LivrariaDbContext> options) : DbContext(options), IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;

        public DbSet<Livro> Livro { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Assunto> Assunto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LivrariaDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            try
            {
                var sucesso = await base.SaveChangesAsync() > 0;
                return sucesso;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction is not null) return;

            _currentTransaction = await Database.BeginTransactionAsync();
        }

        public async Task<bool> CommitTransactionAsync()
        {
            try
            {
                await Commit();
                await _currentTransaction?.CommitAsync()!;
                return true;
            }
            catch(Exception ex)
            {
                await RollbackTransactionAsync();
                return false;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync()!;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
    }
}
