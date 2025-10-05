using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class NotebookCollection
    {
        public int? Id { get; set; }

        public int NotebookId { get; set; }
        public int CollectionId { get; set; }

        public virtual Notebook? Notebook { get; set; } = null!;
        public virtual Collection? Collection { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
