using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class NotebookInsertRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public IFormFile? File { get; set; }


    }
}
