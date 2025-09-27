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
    public class PageController : BaseCRUDController<Model.Model.Page,PageSearchObject,PageInsertRequest,PageUpdateRequest>
    {
        public PageController(IPageService service):base(service)
        {
        }

        [HttpGet("/Page/getAllPages/{notebookId}")]
        public async Task<List<Page>> GetAllPages(int notebookId)
        {
            return await (_service as IPageService).GetAllPagesAsync(notebookId);
        }
        public override  Task<Page> Insert([FromForm]PageInsertRequest request)
        {
            return  base.Insert(request);
        }

        public override  Task<Page> Update(int id,[FromForm] PageUpdateRequest request)
        {
            return  base.Update(id, request);
        }

    }
}
