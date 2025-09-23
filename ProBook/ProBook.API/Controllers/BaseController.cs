using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [Route("[controller]")]
    public class BaseController<TModel,TSearch> : ControllerBase where TSearch:BaseSearchObject where TModel :class
    {
        protected IService<TModel, TSearch> _service;
        public BaseController(IService<TModel,TSearch> service)
        {
            _service = service;
        }
        [HttpGet]
        public virtual PagedResult<TModel> Get([FromQuery] TSearch searchObject)
        {
            return _service.Get(searchObject);
        }

        [HttpGet("{id}")]
        public virtual TModel GetById(int id)
        {
            return _service.GetById(id);
        }
    }
}
