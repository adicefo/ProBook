using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Interface;

namespace ProBook.API.Controllers
{
    [ApiController]
    public class UserController : BaseCRUDController<Model.Model.User,UserSearchObject,UserInsertRequest,UserUpdateRequest>
    {
        public UserController(IUserService service):base(service)
        {
            
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public Model.Model.User Login(string username,string password)
        {
            return (_service as IUserService).Login(username, password);
        }
        [HttpGet("/getCurrentUser")]
        public async Task<Model.Model.User> GetCurrentUser()
        {
            return await (_service as IUserService).GetCurrentUserAsync();
        }
    }
}
