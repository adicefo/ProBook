using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface ISharedNotebookService:ICRUDService<Model.Model.SharedNotebook,SharedNotebookSearchObject,SharedNotebookInsertRequest,SharedNotebookUpdateRequest>
    {
        Task<Tuple<int,List<int>>> GetNumberOfComments(int id);
    }
}
