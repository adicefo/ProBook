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


    }
}
