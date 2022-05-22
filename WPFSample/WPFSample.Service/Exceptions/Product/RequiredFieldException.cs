using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Service.Exceptions.Product
{
    public class RequiredFieldException: BusinessException
    {
        public RequiredFieldException(string message):base(message)
        {
        }
    }
}
