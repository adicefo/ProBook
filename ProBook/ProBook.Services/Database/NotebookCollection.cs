using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class NotebookCollection
    {
        [Key]
        public int Id { get; set; }

        public int NotebookId { get; set; }
        public int CollectionId { get; set; }

        public virtual Notebook Notebook { get; set; } = null!;
        public virtual Collection Collection { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
