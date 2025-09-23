using MapsterMapper;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Database;
using ProBook.Services.Helper;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Service
{
    public class UserService : BaseCRUDService<Model.Model.User, UserSearchObject, Database.User,UserInsertRequest,UserUpdateRequest>, IUserService
    {
        public UserService(ProBookDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<User> AddFilter(UserSearchObject search, IQueryable<User> query)
        {
             var filteredQuery=base.AddFilter(search, query);
             
             if(!string.IsNullOrEmpty(search.Name))
                filteredQuery=filteredQuery.Where(x=>x.Name.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.Surname))
                filteredQuery = filteredQuery.Where(x => x.Surname.Contains(search.Surname));
            if (!string.IsNullOrEmpty(search.Username))
                filteredQuery = filteredQuery.Where(x => x.Username.Contains(search.Username));

            return filteredQuery;

        }

        public override void BeforeInsert(User entity, UserInsertRequest request)
        {
            if (request.Password != request.PasswordConfirm)
                throw new Exception("Passwords do not match");
            entity.RegisteredDate= DateTime.UtcNow;
            entity.PasswordSalt = PasswordGenerate.GenerateSalt();
            var password = PasswordGenerate.GenerateHash(entity.PasswordSalt, request.Password);
            entity.PasswordHash= password;
            base.BeforeInsert(entity, request);


        }

        public override void BeforeUpdate(User entity, UserUpdateRequest request)
        {
            base.BeforeUpdate(entity, request);
        }


    }
}
