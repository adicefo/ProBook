using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int PageId { get; set; }
        public int UserId { get; set; }

        public virtual Page Page { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
