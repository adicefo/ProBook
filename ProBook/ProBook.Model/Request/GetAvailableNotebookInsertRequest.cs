using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class GetAvailableNotebookInsertRequest
    {
        public int? CollectionId { get; set; }
        public int? UserId { get; set; }
    }
}
