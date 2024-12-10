using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Commons.Features
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
