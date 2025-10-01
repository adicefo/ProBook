using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class SharedNotebookInsertRequest
    {
        public bool? IsForEdit { get; set; }

        public int? NotebookId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
    }
}
