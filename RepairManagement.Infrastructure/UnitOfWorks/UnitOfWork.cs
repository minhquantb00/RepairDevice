using Microsoft.EntityFrameworkCore.Storage;
using RepairManagement.Commons.Base;
using RepairManagement.Domain.Repository;
using RepairManagement.Domain.UnitOfWorks;
using RepairManagement.Infrastructure.DataAccess;
using RepairManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Dictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContext _dbContext;

        public UnitOfWork(AppDbContext context,
            IServiceProvider serviceProvider,
            IDbContext dbContext)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _repositories = new Dictionary<Type, object>();
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : BaseEntity<long>
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }
            var repository = new Repository<T>(_context, _dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
