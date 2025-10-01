using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.SearchObject
{
    public class SharedNotebookSearchObject:BaseSearchObject
    {
        public string? NotebookName { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
    }
}
