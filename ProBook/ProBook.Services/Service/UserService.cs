using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProBook.Model.Model;
using ProBook.Model.Request;
using ProBook.Model.SearchObject;
using ProBook.Services.Database;
using ProBook.Services.Exceptions;
using ProBook.Services.Helper;
using ProBook.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ProBook.Services.Exceptions.ValidationException;

namespace ProBook.Services.Service
{
    public class UserService : BaseCRUDService<Model.Model.User, UserSearchObject, Database.User,UserInsertRequest,UserUpdateRequest>, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        IEmailService _emailService;
        public UserService(ProBookDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService) : base(context, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task<bool> UpdatePasswordAsync(int id, UpdatePasswordRequest request)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new NotFoundException($"User with id: {id} not found");
            if (request.Password != request.PasswordConfirm)
                throw new ValidationException($"Password and password confirm must be same value");
            var passwordSalt = PasswordGenerate.GenerateSalt();
            var passwordHash = PasswordGenerate.GenerateHash(passwordSalt, request.Password);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await Context.SaveChangesAsync();
            return true;
        }
        public Model.Model.User Login(string username, string password)
        {
            var entity = Context.Users.FirstOrDefault(x => x.Username == username);

            if (entity == null)
            {
                return null;
            }
            if (!PasswordGenerate.VerifyPassword(password, entity.PasswordHash, entity.PasswordSalt))
                return null;
            return this.Mapper.Map<Model.Model.User>(entity);
        }
        public async Task<Model.Model.User> GetCurrentUserAsync()
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return null;
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            return Mapper.Map<Model.Model.User>(user);

        }
        public override IQueryable<Database.User> AddFilter(UserSearchObject search, IQueryable<Database.User> query)
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

        public override async Task BeforeInsert(Database.User entity, UserInsertRequest request)
        {
            if (request.Password != request.PasswordConfirm)
                throw new ValidationException("Passwords do not match");
            entity.RegisteredDate= DateTime.UtcNow;
            entity.PasswordSalt = PasswordGenerate.GenerateSalt();
            var password = PasswordGenerate.GenerateHash(entity.PasswordSalt, request.Password);
            entity.PasswordHash= password;
           await  base.BeforeInsert(entity, request);


        }

        public async override Task BeforeUpdate(Database.User entity, UserUpdateRequest request)
        {
            await base.BeforeUpdate(entity, request);
        }

        public async Task<Model.Model.LoginResponse?> AuthenticateUserAsync(string username, string password)
        {
            var user = Context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null || !PasswordGenerate.VerifyPassword(password,user.PasswordHash,user.PasswordSalt))
                return null;

            if ((bool)user.TwoFactorAuthEnabled)
            {
                var code = new Random().Next(100000, 999999).ToString();
                user.TwoFactorCode = code;
                user.TwoFactorCodeExpiresAt = DateTime.UtcNow.AddMinutes(5);
                await Context.SaveChangesAsync();

                await _emailService.SendEmailAsync(
                    user.Email,
                    "Your verification code",
                    $"Your 2FA code is {code}. It will expire in 5 minutes."
                );

                return new LoginResponse
                {
                    RequiresTwoFactor = true,
                    Message = "2FA code sent to your email."
                };
            }

            return new LoginResponse
            {
                RequiresTwoFactor = false,
                Message = "Login successful."
            };
        }

        public async Task<Model.Model.LoginResponse?> VerifyTwoFactorAsync(string username, string code)
        {
            var user = Context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null || user.TwoFactorCode != code || user.TwoFactorCodeExpiresAt < DateTime.UtcNow)
                return new LoginResponse { Message = "Invalid or expired code." };

            user.TwoFactorCode = null;
            user.TwoFactorCodeExpiresAt = null;
            await Context.SaveChangesAsync();

            
            return new LoginResponse { Message = "2FA verified. Login successful." };
        }
    }
}
