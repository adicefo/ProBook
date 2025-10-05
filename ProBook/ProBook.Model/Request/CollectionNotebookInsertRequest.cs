using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class CollectionNotebookInsertRequest
    {
        public int? NotebookId { get; set; }
        public int? CollectionId { get; set; }
    }
}
