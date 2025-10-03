using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface ICommentService:ICRUDService<Model.Model.Comment,CommentSearchObject,CommentInsertRequest,CommentUpdateRequest>
    {
        Task<List<Model.Model.Comment>> GetAllComments(int pageId);
        Task<List<Model.Model.Comment>> UpdateViewed(List<int> commentIds);
    }
}
