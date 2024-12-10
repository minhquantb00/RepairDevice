using RepairManagement.Commons.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Commons.Exceptions
{
    public class InvalidException : Exception
    {

        public string Name { get; private set; }

        public InvalidException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(Constant.ExceptionMessage.INVALID, Name);

    }
}
