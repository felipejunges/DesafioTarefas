using DesafioTarefas.Domain.UnitsOfWork;
using DesafioTarefas.Infra.Contexts;

namespace DesafioTarefas.Infra.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DesafioContext _db;

        public UnitOfWork(DesafioContext db)
        {
            _db = db;
        }

        public Task BeginTransactionAsync()
        {
            return _db.Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            return _db.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync()
        {
            return _db.Database.RollbackTransactionAsync();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
