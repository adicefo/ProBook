using Google.Cloud.Storage.V1;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Exceptions;
using ProBook.Services.Helper;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProBook.Services.Service
{
    public class PageService : BaseCRUDService<Model.Model.Page, PageSearchObject, Database.Page, PageInsertRequest, PageUpdateRequest>, IPageService
    {

        private readonly ImageUploadHelper _imageUploadHelper;
        public PageService(Database.ProBookDBContext context, IMapper mapper,ImageUploadHelper imageUploadHelper ) : base(context, mapper)
        {
            _imageUploadHelper = imageUploadHelper;
        }

        public async Task<List<Page>> GetAllPagesAsync(int notebookId)
        {
            var pages = await Context.Pages.Where(x => x.NotebookId==notebookId)
                .Include(x => x.Notebook)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
            if(pages!=null)
            {
                var result = Mapper.Map<List<Model.Model.Page>>(pages);
                return result;
            }
            return null;

        }

        public override IQueryable<Database.Page> AddFilter(PageSearchObject search, IQueryable<Database.Page> query)
        {
            var filteredQuery= base.AddFilter(search, query);
            filteredQuery = filteredQuery.Include(x => x.Notebook);
            return filteredQuery;
        }
        public override IQueryable<Database.Page> AddInclude(IQueryable<Database.Page> query)
        {
            return query.Include(x => x.Notebook);
        }

        public override async Task BeforeInsert(Database.Page entity, PageInsertRequest request)
        {
            entity.CreatedAt = DateTime.UtcNow;
            Database.Notebook notebook = await Context.Notebooks.FindAsync(request.NotebookId);
            if (notebook == null)
                throw new NotFoundException($"Notebook with id {request.NotebookId} not found");
            entity.Notebook = notebook;

            if (request.File != null)
                entity.ImageUrl = await _imageUploadHelper.UploadFileAsync(request.File);

            

            await base.BeforeInsert(entity, request);
        }
        public override async Task BeforeUpdate(Database.Page entity, PageUpdateRequest request)
        {
            
            if (request.File != null)
                entity.ImageUrl = await _imageUploadHelper.UploadFileAsync(request.File);

            await base.BeforeUpdate(entity, request);
        }
    }
}
