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
    public class NotebookController : BaseCRUDController<Model.Model.Notebook,NotebookSearchObject,NotebookInsertRequest,NotebookUpdateRequest>
    {
        public NotebookController(INotebookService service):base(service)
        {
        }
        [HttpGet("/Notebook/getAllNotebooks/{userId}")]
        public async Task<List<Model.Model.Notebook>>GetAllNotebooks(int userId)
        {
            return await (_service as INotebookService).GetAllNotebooks(userId);
        }

        public override  Task<Notebook> Insert([FromForm]NotebookInsertRequest request)
        {
            return base.Insert(request);
        }

        public override async Task<Notebook> Update(int id, [FromForm] NotebookUpdateRequest request)
        {
            return await base.Update(id, request);
        }

    }
}
