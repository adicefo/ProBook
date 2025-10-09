using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Helper;
using ProBook.Model.Model;
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

        [AllowAnonymous]
        public override  Task<User> Insert(UserInsertRequest request)
        {
            return  base.Insert(request);

        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public Model.Model.User Login(string username,string password)
        {
            return (_service as IUserService).Login(username, password);
        }
        [HttpGet("/User/getCurrentUser")]
        public async Task<Model.Model.User> GetCurrentUser()
        {
            return await (_service as IUserService).GetCurrentUserAsync();
        }
        [HttpPost("/User/updatePassword/{id}")]
        public async Task<bool> UpdatePasswordAsync(int id,UpdatePasswordRequest request)
        {
            return await (_service as IUserService).UpdatePasswordAsync(id,request);
        }
    }
}
