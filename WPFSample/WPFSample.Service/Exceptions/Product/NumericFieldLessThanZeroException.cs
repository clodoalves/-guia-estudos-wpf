using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Service.Exceptions.Product
{
    public class NumericFieldLessThanZeroException : BusinessException
    {
        public NumericFieldLessThanZeroException(string message) : base(message)
        {

        }
    }
}
