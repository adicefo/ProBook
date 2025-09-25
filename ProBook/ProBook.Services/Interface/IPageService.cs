using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface IPageService:ICRUDService<Model.Model.Page,PageSearchObject,PageInsertRequest,PageUpdateRequest>
    {
        public List<Page> GetAllPages(int notebookId);
    }
}
