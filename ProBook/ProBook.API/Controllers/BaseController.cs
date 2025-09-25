using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class BaseController<TModel,TSearch> : ControllerBase where TSearch:BaseSearchObject where TModel :class
    {
        protected IService<TModel, TSearch> _service;
        public BaseController(IService<TModel,TSearch> service)
        {
            _service = service;
        }
        [HttpGet]
        public virtual async Task<PagedResult<TModel>> Get([FromQuery] TSearch searchObject)
        {
            return await _service.GetAsync(searchObject);
        }

        [HttpGet("{id}")]
        public virtual async Task<TModel> GetById(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
