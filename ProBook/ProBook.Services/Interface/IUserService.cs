using Microsoft.AspNetCore.Mvc;
using ProBook.Model.Model;
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
        Task<LoginResponse?> AuthenticateUserAsync(string username, string password);

        Task<LoginResponse?> VerifyTwoFactorAsync(string username, string code);
        Model.Model.User Login(string username, string password);
        Task<Model.Model.User> GetCurrentUserAsync();

        Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request);
    }
}
