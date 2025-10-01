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
    public class CommentController : BaseCRUDController<Model.Model.Comment,CommentSearchObject,CommentInsertRequest,CommentUpdateRequest>
    {
        public CommentController(ICommentService service):base(service)
        {
        }

        [HttpGet("/Comment/getAllComments/{pageId}")]
        public async Task<List<Comment>> GetAllComments(int pageId)
        {
            return await (_service as ICommentService).GetAllComments(pageId);
        }

      

    }
}
