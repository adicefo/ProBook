using Google.Cloud.Storage.V1;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Database;
using ProBook.Services.Helper;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProBook.Services.Service
{
    public class NotebookService : BaseCRUDService<Model.Model.Notebook, NotebookSearchObject, Database.Notebook, NotebookInsertRequest, NotebookUpdateRequest>, INotebookService
    {
        private readonly ImageUploadHelper _imageUploadHelper;
        public NotebookService(ProBookDBContext context, IMapper mapper,ImageUploadHelper imageUploadHelper) : base(context, mapper)
        {
            _imageUploadHelper = imageUploadHelper;
        }

        public async Task<List<Model.Model.Notebook>> GetAllNotebooks(int userId)
        {
            var notebooks = await Context.Notebooks.
                Where(x => x.UserId == userId)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            if(notebooks!=null)
            {
                var result = Mapper.Map <List<Model.Model.Notebook>>(notebooks);
                return result;
            }
            return null;

        }
        public override  IQueryable<Notebook> AddFilter(NotebookSearchObject search, IQueryable<Notebook> query)
        {
            var filteredQuery= base.AddFilter(search, query);

            if (!string.IsNullOrEmpty(search.Name))
                filteredQuery = filteredQuery.Where(x => x.Name.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.Description))
                filteredQuery = filteredQuery.Where(x => x.Description.Contains(search.Description));

            filteredQuery = filteredQuery.Include(x => x.User);

            return filteredQuery;
        }
        public override IQueryable<Notebook> AddInclude(IQueryable<Notebook> query)
        {
            return query.Include(x => x.User);
        }


        public override async Task BeforeInsert(Notebook entity, NotebookInsertRequest request)
        {
            entity.CreatedAt= DateTime.UtcNow;

            User user = await Context.Users.FindAsync(request.UserId);
            if (user == null)
                throw new Exception("Entity not found");

            entity.User = user;

            if (request.File != null)
                entity.ImageUrl = await _imageUploadHelper.UploadFileAsync(request.File);
            else
                entity.ImageUrl = null;
            
            await base.BeforeInsert(entity, request);

        }

        public override async Task BeforeUpdate(Notebook entity, NotebookUpdateRequest request)
        {
            if (request.File != null)
                entity.ImageUrl = await _imageUploadHelper.UploadFileAsync(request.File);
            await base.BeforeUpdate(entity, request);
        }

        
    }
}
