using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Bussiness.Exceptions
{
   public  class FileSizeException:Exception
    {
        public FileSizeException(string propertyName,string? message) : base(message)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

    }
}
