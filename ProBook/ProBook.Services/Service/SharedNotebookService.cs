using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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
    public class SharedNotebookService : BaseCRUDService<Model.Model.SharedNotebook, SharedNotebookSearchObject, Database.SharedNotebook, SharedNotebookInsertRequest, SharedNotebookUpdateRequest>
        , ISharedNotebookService
    {
        public SharedNotebookService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<SharedNotebook> AddFilter(SharedNotebookSearchObject search, IQueryable<SharedNotebook> query)
        {
            var filteredQuery= base.AddFilter(search, query);
            if(!string.IsNullOrEmpty(search.NotebookName))
                filteredQuery=filteredQuery.Where(x=>x.Notebook.Name.Contains(search.NotebookName));
            if (search.FromUserId.HasValue)
                filteredQuery = filteredQuery.Where(x => x.FromUserId == search.FromUserId);
            if(search.ToUserId.HasValue)
                filteredQuery = filteredQuery.Where(x => x.ToUserId == search.ToUserId);
            filteredQuery.Include(x => x.Notebook).Include(x=>x.FromUser).Include(x=>x.ToUser);
            
            return filteredQuery;

        }

        public override IQueryable<SharedNotebook> AddInclude(IQueryable<SharedNotebook> query)
        {
            
            return query.Include(x=>x.Notebook).Include(x=>x.FromUser).Include(x=>x.ToUser);
        }

        public override async Task BeforeInsert(SharedNotebook entity, SharedNotebookInsertRequest request)
        {
            entity.SharedDate= DateTime.UtcNow;
            Database.Notebook notebook = await Context.Notebooks.FindAsync(request.NotebookId);
            if (notebook == null)
                throw new Exception("Entity not found");
            entity.Notebook = notebook;

            Database.User fromUser= await Context.Users.FindAsync(request.FromUserId);
            if (fromUser == null)
                throw new Exception("Entity not found");
            entity.FromUser = fromUser;

            Database.User toUser = await Context.Users.FindAsync(request.ToUserId);
            if (toUser == null)
                throw new Exception("Entity not found");
            entity.ToUser = toUser;



            await base.BeforeInsert(entity, request);

        }
        public override async Task BeforeUpdate(SharedNotebook entity, SharedNotebookUpdateRequest request)
        {
            await base.BeforeUpdate(entity, request);
        }
    }
}
