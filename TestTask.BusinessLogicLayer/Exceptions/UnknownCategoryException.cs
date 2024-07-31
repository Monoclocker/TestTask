using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.BusinessLogicLayer.Exceptions
{
    public class UnknownEntityException: Exception
    {
        public UnknownEntityException(string message): base(message) { }
    }
}
