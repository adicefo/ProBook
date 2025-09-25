using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface INotebookService:ICRUDService<Model.Model.Notebook,NotebookSearchObject,NotebookInsertRequest,NotebookUpdateRequest>
    {

    }
}
