using MapsterMapper;
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

        public TModel Insert(TInsert request)
        {
            var set=Context.Set<TDbEntity>();
            TDbEntity entity = Mapper.Map<TDbEntity>(request);
            Mapper.Map(request, entity);
            BeforeInsert(entity, request);
            set.Add(entity);
            Context.SaveChanges();
            var result = Mapper.Map<TModel>(entity);
            return result;
            
        }

        public TModel Update(int id,TUpdate request)
        {
            var set=Context.Set<TDbEntity>();
            var entity = set.Find(id);
            if (entity == null)
                throw new Exception("Entity not found");
            entity = Mapper.Map(request, entity);
            BeforeUpdate(entity, request);
            Context.SaveChanges();
            var result=Mapper.Map<TModel>(entity);
            return result;
        }

        public TModel Delete(int id)
        {
            var set= Context.Set<TDbEntity>();
            var entity = set.Find(id);
            if (entity == null)
                throw new Exception("Entity not found");
            set.Remove(entity);
            Context.SaveChanges();
            var result= Mapper.Map<TModel>(entity);
            return result;
        }
        public virtual void BeforeInsert(TDbEntity entity,TInsert request)
        {

        }

        public virtual void BeforeUpdate(TDbEntity entity,TUpdate request)
        {

        }
    }
}
