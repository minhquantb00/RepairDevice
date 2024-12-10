using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Repository
{
    public interface ISpecificationFactory
    {
        ISpecification<T> Create<T>();
    }
}
