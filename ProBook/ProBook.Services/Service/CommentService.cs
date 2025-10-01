using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Database;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Service
{
    public class CommentService : BaseCRUDService<Model.Model.Comment, CommentSearchObject, Database.Comment,
        CommentInsertRequest, CommentUpdateRequest>, ICommentService
    {
        public CommentService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<Model.Model.Comment>> GetAllComments(int pageId)
        {
            var comments = await Context.Comments.Where(x => x.PageId == pageId)
             .Include(x => x.Page)
             .Include(x=>x.User)
             .OrderBy(x => x.CreatedAt)
             .ToListAsync();
            if (comments != null)
            {
                var result = Mapper.Map<List<Model.Model.Comment>>(comments);
                return result;
            }
            return null;
        }

        public override IQueryable<Database.Comment> AddFilter(CommentSearchObject search, IQueryable<Database.Comment> query)
        {
            var filteredQuery = base.AddFilter(search, query);
            if (search.PageId.HasValue)
                filteredQuery = filteredQuery.Where(x => x.PageId == search.PageId);
            return filteredQuery;
        }
        public override IQueryable<Database.Comment> AddInclude(IQueryable<Database.Comment> query)
        {
            return query.Include(x => x.Page).Include(x=>x.User);
        }
        public override async Task BeforeInsert(Database.Comment entity, CommentInsertRequest request)
        {
            entity.CreatedAt= DateTime.UtcNow;
            Database.Page page = await Context.Pages.FindAsync(request.PageId);
            if (page == null)
                throw new Exception("Entity not found");
            entity.Page = page;

            Database.User user = await Context.Users.FindAsync(request.UserId);
            if (user == null)
                throw new Exception("Entity not found");
            entity.User = user;

            await base.BeforeInsert(entity, request);
        }

        public override async Task BeforeUpdate(Database.Comment entity, CommentUpdateRequest request)
        {
            await base.BeforeUpdate(entity, request);
        }
    }
}
