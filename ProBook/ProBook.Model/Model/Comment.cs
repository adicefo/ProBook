using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? PageId { get; set; }
        public int? UserId { get; set; }
        public int? SharedNotebookId { get; set; }
        public bool? Viewed { get; set; }
        public virtual Model.Page? Page { get; set; } = null!;
        public virtual Model.User? User { get; set; } = null!;
        public virtual Model.SharedNotebook? SharedNotebook { get; set; } = null!;
    }
}
