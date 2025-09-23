using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [Route("[controller]")]
    public class BaseCRUDController<TModel,TSearch,TInsert,TUpdate> :BaseController<TModel,TSearch> where TModel : class where TSearch : BaseSearchObject
    {
        protected new ICRUDService<TModel, TSearch, TInsert, TUpdate> _service;

        public BaseCRUDController(ICRUDService<TModel,TSearch,TInsert,TUpdate> service):base(service) {
        _service = service;
            
        }

        [HttpPost]
        public virtual TModel Insert(TInsert request)
        {
            return _service.Insert(request);
        }

        [HttpPut]
        public virtual TModel Update(int id,TUpdate request)
        {
            return _service.Update(id,request);
        }

        [HttpDelete]
        public virtual TModel Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
