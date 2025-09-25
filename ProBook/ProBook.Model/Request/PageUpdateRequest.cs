using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Model.Request
{
    public class PageUpdateRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public IFormFile? File { get; set; }


    }
}
