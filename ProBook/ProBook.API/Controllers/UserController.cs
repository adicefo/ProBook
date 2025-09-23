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

        
    }
}
