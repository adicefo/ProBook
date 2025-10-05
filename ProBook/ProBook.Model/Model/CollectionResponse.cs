using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class CollectionResponse
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }

        public virtual User? User { get; set; } = null!;

        public List<Notebook>? Notebooks { get; set; }
    }
}
