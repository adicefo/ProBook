using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [ApiController]
    public class CollectionController : BaseCRUDController<Model.Model.Collection, CollectionSearchObject, CollectionInsertRequest, CollectionUpdateRequest>
    {
        public CollectionController(ICollectionService service) : base(service)
        {
        }

        [HttpGet("/Collection/getCollectionResponse/{userId}")]
        public async Task<List<CollectionResponse>>GetCollectionResponse(int userId)
        {
            return await (_service as ICollectionService).GetCollectionResponse(userId);
        }

        [HttpPost("/Collection/addToCollection")]
        public Task<Model.Model.NotebookCollection> AddToCollection([FromBody]CollectionNotebookRequest request)
        {
            return (_service as ICollectionService).AddToCollection(request);
        }
        [HttpDelete("/Collection/removeFromCollection")]
        public Task<Model.Model.NotebookCollection> RemoveFromCollection([FromBody] CollectionNotebookRequest request)
        {
            return (_service as ICollectionService).RemoveFromCollection(request);
        }
    }
}
