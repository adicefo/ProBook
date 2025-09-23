using MapsterMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProBook.Model.Helper;
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
    public abstract class BaseService<TModel, TSearch, TDbEntity> : IService<TModel, TSearch> where TModel : class where TSearch : BaseSearchObject where TDbEntity : class
    {
        public ProBookDBContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public BaseService(ProBookDBContext context,IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public PagedResult<TModel> Get(TSearch search)
        {
            List<TModel> result = new List<TModel>();
            var query = Context.Set<TDbEntity>().AsQueryable();

            query = AddFilter(search, query);
            query = AddInclude(search, query);

            int count = query.Count();

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
                query = query.Skip(search.Page.Value * search.PageSize.Value)
                 .Take(search.PageSize.Value);

            var list = query.ToList();

            result = Mapper.Map(list, result);

            PagedResult<TModel> pageResult=new PagedResult<TModel>();
            pageResult.Result= result;
            pageResult.Count= count;
            return pageResult;
        }

        public TModel GetById(int id)
        {
            var entity = Context.Set<TDbEntity>().Find(id);
            if (entity == null)
                throw new Exception("Entity not found");
            return Mapper.Map<TModel>(entity);
        }

        public virtual IQueryable<TDbEntity> AddFilter(TSearch search, IQueryable<TDbEntity> query)
        {
            return query;
        }
        public virtual IQueryable<TDbEntity> AddInclude(TSearch search, IQueryable<TDbEntity> query)
        {
            return query;
        }
    }
}
