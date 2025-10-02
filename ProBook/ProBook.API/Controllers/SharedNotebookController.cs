using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [ApiController]
    public class SharedNotebookController : BaseCRUDController<Model.Model.SharedNotebook,SharedNotebookSearchObject,
        SharedNotebookInsertRequest,SharedNotebookUpdateRequest>
    {
        public SharedNotebookController(ISharedNotebookService service):base(service)
        {
        }

        [HttpGet("/SharedNotebook/getNumberOfComments/{id}")]
        public async Task<Tuple<int,List<int>>> GetNumberOfComments(int id)
        {
            return await (_service as ISharedNotebookService).GetNumberOfComments(id);
        }

    }
}
