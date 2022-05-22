using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Service.Exceptions
{
    public abstract class BusinessException: Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
}
