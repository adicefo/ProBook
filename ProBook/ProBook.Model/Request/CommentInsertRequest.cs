using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class CommentInsertRequest
    {
        public string? Content { get; set; }
        public int? PageId { get; set; }
        public int? UserId { get; set; }
    }
}
