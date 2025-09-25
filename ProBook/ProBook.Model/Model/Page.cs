using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class Page
    { 
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }
        public int NotebookId { get; set; }

        public virtual Notebook Notebook { get; set; } = null!;
    }
}
