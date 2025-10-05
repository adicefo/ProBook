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
    public class CollectionService : BaseCRUDService<Model.Model.Collection, CollectionSearchObject, Database.Collection,
        CollectionInsertRequest, CollectionUpdateRequest>, ICollectionService
    {
        public CollectionService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Model.Model.NotebookCollection> AddToCollection(CollectionNotebookInsertRequest request)
        {
            Database.Notebook notebook = await Context.Notebooks.FirstOrDefaultAsync(x => x.Id == request.NotebookId);
            if (notebook == null)
                throw new Exception("Sent invalid data,entity not found");
            Database.Collection collection= await Context.Collections.FirstOrDefaultAsync(x=>x.Id== request.CollectionId);
            if(collection == null)
                throw new Exception("Sent invalid data,entity not found");

            var checkElement = await Context.NotebookCollections.
                Where(x => x.NotebookId == request.NotebookId && x.CollectionId == request.CollectionId)
                .AnyAsync();
            if (checkElement)
                throw new Exception("Notebook already in collection");


            Database.NotebookCollection entity = new Database.NotebookCollection();
            entity.NotebookId = request.NotebookId??0;
            entity.CollectionId = request.CollectionId??0;
            entity.Notebook = notebook;
            entity.Collection = collection;
            entity.CreatedAt= DateTime.UtcNow;

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<Model.Model.NotebookCollection>(entity);
            
        }
        public async Task<List<CollectionResponse>> GetCollectionResponse(int userId)
        {
            var collections = await Context.Collections.
                Where(x => x.UserId == userId)
                .ToListAsync();
            if (collections == null)
                return null;
            var response = new List<CollectionResponse>();
            collections.ForEach(async x =>
            {
                var notebooks=await Context.NotebookCollections
                .Where(nc=>nc.CollectionId==x.Id)
                .Select(nc=>nc.Notebook)
                .ToListAsync();

                response.Add(new CollectionResponse
                {
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt=x.CreatedAt,
                    User=Mapper.Map<Model.Model.User>(x.User),
                    UserId=x.UserId,
                    Notebooks=notebooks==null?null:Mapper.Map<List<Model.Model.Notebook>>(notebooks)
                });

            });
            return response;

            
        }

        public override IQueryable<Database.Collection> AddFilter(CollectionSearchObject search, IQueryable<Database.Collection> query)
        {
            var filteredQuery = base.AddFilter(search, query);

            if (!string.IsNullOrEmpty(search.Name))
                filteredQuery = filteredQuery.Where(x => x.Name.Contains(search.Name));
            if (search.UserId.HasValue)
                filteredQuery = filteredQuery.Where(x => x.UserId==search.UserId);

            filteredQuery = filteredQuery.Include(x => x.User);

            return filteredQuery;
        }

        public override IQueryable<Database.Collection> AddInclude(IQueryable<Database.Collection> query)
        {
            return query.Include(x => x.User);
        }

        public override async Task BeforeInsert(Database.Collection entity, CollectionInsertRequest request)
        {
            entity.CreatedAt = DateTime.UtcNow;

            Database.User user = await Context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
                throw new Exception("Entity not found");

            entity.User = user;
            await base.BeforeInsert(entity, request);


        }

        public override async Task BeforeUpdate(Database.Collection entity, CollectionUpdateRequest request)
        {
            await  base.BeforeUpdate(entity, request);
        }

        
    }
}
