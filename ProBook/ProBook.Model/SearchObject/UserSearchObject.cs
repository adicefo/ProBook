using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.SearchObject
{
    public class UserSearchObject:BaseSearchObject
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? Username { get; set; }
    }
}
