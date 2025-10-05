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
    public interface ICollectionService:ICRUDService<Model.Model.Collection,CollectionSearchObject,
        CollectionInsertRequest,CollectionUpdateRequest>
    {
        Task<NotebookCollection> AddToCollection(CollectionNotebookInsertRequest request);
        Task<List<CollectionResponse> GetCollectionResponse(int userId);
    }
}
