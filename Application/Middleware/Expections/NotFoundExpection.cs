using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware.Expections
{
    public class NotFoundExpection :Exception
    {
        public NotFoundExpection(string message):base(message) { }
    }
}
