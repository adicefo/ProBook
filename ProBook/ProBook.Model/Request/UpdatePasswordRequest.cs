using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class UpdatePasswordRequest
    {
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;

    }
}
