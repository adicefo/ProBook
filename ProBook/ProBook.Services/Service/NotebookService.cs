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
        private readonly StorageClient _storageClient;
        public NotebookService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {
            _storageClient = StorageClient.Create();
        }

        public override IQueryable<Notebook> AddFilter(NotebookSearchObject search, IQueryable<Notebook> query)
        {
            var filteredQuery= base.AddFilter(search, query);

            if (!string.IsNullOrEmpty(search.Name))
                filteredQuery = filteredQuery.Where(x => x.Name.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.Description))
                filteredQuery = filteredQuery.Where(x => x.Description.Contains(search.Description));

            return filteredQuery;
        }
        public override IQueryable<Notebook> AddInclude(NotebookSearchObject search, IQueryable<Notebook> query)
        {
            var filteredQuery = base.AddInclude(search, query);
            filteredQuery = EntityFrameworkQueryableExtensions.Include(filteredQuery, x => x.User);
            return filteredQuery;
        }

        public override async void BeforeInsert(Notebook entity, NotebookInsertRequest request)
        {
            entity.CreatedAt= DateTime.UtcNow;

            if(request.File!=null)
                entity.ImageUrl = await ImageUploadHelper.UploadFileAsync(request.File,_storageClient);

            base.BeforeInsert(entity, request);
        }

        public override async void BeforeUpdate(Notebook entity, NotebookUpdateRequest request)
        {
            if (request.File != null)
                entity.ImageUrl = await ImageUploadHelper.UploadFileAsync(request.File, _storageClient);
        }
    }
}
