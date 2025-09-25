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
        public virtual async Task<TModel> Insert(TInsert request)
        {
            return await _service.InsertAsync(request);
        }

        [HttpPut]
        public virtual async Task<TModel> Update(int id,TUpdate request)
        {
            return await _service.UpdateAsync(id, request);
        }

        [HttpDelete]
        public virtual async Task<TModel> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
