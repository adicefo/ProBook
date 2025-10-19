using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class TwoFactorRequest
    {
        public string Username { get; set; }
        public string Code { get; set; }
    }
}
