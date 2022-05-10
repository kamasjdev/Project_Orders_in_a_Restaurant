using Restaurant.ApplicationLogic.Interfaces;
using System.Data;

namespace Restaurant.Infrastructure.DAL
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction = null;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbConnection Connection => _dbConnection;

        public IDbTransaction Transaction => _dbTransaction;

        public void Begin()
        {
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }

            _dbTransaction = null;
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            Dispose();
        }
    }
}
