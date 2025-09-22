using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class Notebook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public ICollection<NotebookCollection> NotebookCollections { get; set; } = new List<NotebookCollection>();
        public ICollection<SharedNotebook> SharedNotebooks { get; set; } = new List<SharedNotebook>();
        public ICollection<Page> Pages { get; set; } = new List<Page>();

    }
}
