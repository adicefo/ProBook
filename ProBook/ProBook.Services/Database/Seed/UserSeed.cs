using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class UserSeed
    {
        public static void SeedData(this EntityTypeBuilder<User> entity)
        {
            entity.HasData(
                new User()
                {
                    Id = 1,
                    Name="User1",
                    Surname="User1",
                    Username="user1",
                    Email="user1@gmail.com",
                    PasswordSalt= "qYk4OxryQgplthbzFlS0yQ==",
                    PasswordHash= "UbzzxOGag4pPmBhguTkyKnpEZw4=",
                    TelephoneNumber="061-234-444",
                    Gender="Male",
                    RegisteredDate= new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
                    IsStudent=true
                },
                new User()
                {
                    Id = 2,
                    Name = "User2",
                    Surname = "User2",
                    Username = "user2",
                    Email = "user2@gmail.com",
                    PasswordSalt = "qYk4OxryQgplthbzFlS0yQ==",
                    PasswordHash = "UbzzxOGag4pPmBhguTkyKnpEZw4=",
                    TelephoneNumber = "063-234-444",
                    Gender = "Male",
                    RegisteredDate = new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
                    IsStudent = true
                }, new User()
                {
                    Id = 3,
                    Name = "User3",
                    Surname = "User3",
                    Username = "user3",
                    Email = "user3@gmail.com",
                    PasswordSalt = "qYk4OxryQgplthbzFlS0yQ==",
                    PasswordHash = "UbzzxOGag4pPmBhguTkyKnpEZw4=",
                    TelephoneNumber = "065-234-444",
                    Gender = "Female",
                    RegisteredDate = new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
                    IsStudent = true
                }
                );

        }

    }
}
