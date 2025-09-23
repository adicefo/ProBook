using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface IUserService:ICRUDService<Model.Model.User,UserSearchObject,UserInsertRequest,UserUpdateRequest>
    {

    }
}
