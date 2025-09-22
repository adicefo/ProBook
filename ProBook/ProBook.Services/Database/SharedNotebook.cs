using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class SharedNotebook
    {
        [Key]
        public int Id { get; set; }

        public DateTime? SharedDate { get; set; }

        public bool? IsForEdit { get; set; }

        public int NotebookId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public virtual Notebook Notebook { get; set; } = null!;
        public virtual User FromUser { get; set; } = null!;
        public virtual User ToUser { get; set; } = null!;

    }
}
