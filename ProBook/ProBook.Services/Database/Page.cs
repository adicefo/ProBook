using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class Page
    {

        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }
        public int NotebookId { get; set; }

        public virtual Notebook Notebook { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
