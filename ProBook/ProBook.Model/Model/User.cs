using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class User
    {
        
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

 
        public string? Username { get; set; }

        public string? Email { get; set; }

        public DateTime? RegisteredDate { get; set; }


        public bool? IsStudent { get; set; }
    
        public string? TelephoneNumber { get; set; }

        public string? Gender { get; set; }
    }
}
