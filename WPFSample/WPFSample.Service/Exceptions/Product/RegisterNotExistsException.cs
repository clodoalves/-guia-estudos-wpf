using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Service.Exceptions.Product
{
    public class RegisterNotExistsException : BusinessException
    {
        public RegisterNotExistsException(string message) : base(message)
        {
        }
    }
}
