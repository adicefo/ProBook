using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class LoginResponse
    {
        public bool? RequiresTwoFactor { get; set; }
        public string? Message { get; set; }
    }
}
