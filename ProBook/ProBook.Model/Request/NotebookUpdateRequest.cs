using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class NotebookUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
