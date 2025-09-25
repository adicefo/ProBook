using Google.Cloud.Storage.V1;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
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
            var pages =Context.Pages.Where(x => x.NotebookId == notebookId).OrderBy(x => x.CreatedAt).ToList();
            if(pages!=null)
            {
                var result= Mapper.Map<List<Model.Model.Page>>(pages);
                return result;
            }
            return null;

        }

        public override IQueryable<Database.Page> AddFilter(PageSearchObject search, IQueryable<Database.Page> query)
        {
            return base.AddFilter(search, query);
        }
        public override IQueryable<Database.Page> AddInclude(PageSearchObject search, IQueryable<Database.Page> query)
        {
            var filteredQuery = base.AddInclude(search, query);
            filteredQuery = EntityFrameworkQueryableExtensions.Include(filteredQuery, x => x.Notebook);
            return filteredQuery;
        }
        public override async Task BeforeInsert(Database.Page entity, PageInsertRequest request)
        {
            entity.CreatedAt = DateTime.UtcNow;

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
