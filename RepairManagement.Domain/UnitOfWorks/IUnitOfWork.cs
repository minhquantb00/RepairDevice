using Microsoft.EntityFrameworkCore.Storage;
using RepairManagement.Commons.Base;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<T> Repository<T>() where T : BaseEntity<long>;

        void SaveChanges();

        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
    }
}
