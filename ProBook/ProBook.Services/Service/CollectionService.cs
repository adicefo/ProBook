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
    public class CollectionService : BaseCRUDService<Model.Model.Collection, CollectionSearchObject, Database.Collection,
        CollectionInsertRequest, CollectionUpdateRequest>, ICollectionService
    {
        public CollectionService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Collection> AddFilter(CollectionSearchObject search, IQueryable<Collection> query)
        {
            var filteredQuery = base.AddFilter(search, query);

            if (!string.IsNullOrEmpty(search.Name))
                filteredQuery = filteredQuery.Where(x => x.Name.Contains(search.Name));
            if (search.UserId.HasValue)
                filteredQuery = filteredQuery.Where(x => x.UserId==search.UserId);

            filteredQuery = filteredQuery.Include(x => x.User);

            return filteredQuery;
        }

        public override IQueryable<Collection> AddInclude(IQueryable<Collection> query)
        {
            return query.Include(x => x.User);
        }

        public override async Task BeforeInsert(Collection entity, CollectionInsertRequest request)
        {
            entity.CreatedAt = DateTime.UtcNow;

            Database.User user = await Context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
                throw new Exception("Entity not found");

            entity.User = user;
            await base.BeforeInsert(entity, request);


        }

        public override async Task BeforeUpdate(Collection entity, CollectionUpdateRequest request)
        {
            await  base.BeforeUpdate(entity, request);
        }
    }
}
