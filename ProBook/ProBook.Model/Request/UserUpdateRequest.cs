using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class UserUpdateRequest
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }


        public string? TelephoneNumber { get; set; }

        public string? Gender { get; set; }

        public bool? IsStudent { get; set; }
    }
}
