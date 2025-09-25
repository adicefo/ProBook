using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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
    public abstract class BaseCRUDService<TModel, TSearch, TDbEntity, TInsert, TUpdate> : BaseService<TModel, TSearch, TDbEntity> where TModel : class where TSearch : BaseSearchObject where TDbEntity:class   
    {
        public BaseCRUDService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public virtual async Task<TModel> InsertAsync(TInsert request)
        {
            var set=Context.Set<TDbEntity>();
            TDbEntity entity = Mapper.Map<TDbEntity>(request);
            Mapper.Map(request, entity);
            await BeforeInsert(entity, request);
            set.Add(entity);
            await Context.SaveChangesAsync();
            
            return Mapper.Map<TModel>(entity); 
            
        }

        

        public virtual async Task<TModel> UpdateAsync(int id,TUpdate request)
        {
            var set=Context.Set<TDbEntity>();
            var entity = set.Find(id);
            if (entity == null)
                throw new Exception("Entity not found");
            entity = Mapper.Map(request, entity);
            await BeforeUpdate(entity, request);
            await Context.SaveChangesAsync();
            return Mapper.Map<TModel>(entity);
        }

      

        public virtual async Task<TModel> DeleteAsync(int id)
        {
            var set= Context.Set<TDbEntity>();
            var entity = await set.FindAsync(id);
            if (entity == null)
                throw new Exception("Entity not found");
             set.Remove(entity);
            await Context.SaveChangesAsync();
            return Mapper.Map<TModel>(entity);
        }

        

        public virtual async Task BeforeInsert(TDbEntity entity,TInsert request)
        {

        }

        public virtual async Task BeforeUpdate(TDbEntity entity,TUpdate request)
        {

        }
    }
}
