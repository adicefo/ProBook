using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _service { get; set; }
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public PagedResult<Model.Model.User> Get([FromQuery] UserSearchObject search)
        {
            return _service.Get(search);
        }
        [HttpGet("{id}")]
        public Model.Model.User GetById(int id)
        {
            return _service.GetById(id);
        }
    }
}
