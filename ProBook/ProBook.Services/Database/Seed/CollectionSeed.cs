using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class CollectionSeed
    {
        public static void SeedData(this EntityTypeBuilder<Collection> entity)
        {
            entity.HasData(
                new Collection()
                {
                    Id = 1,
                    Name = "Math-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 14, 22, 49, 05, DateTimeKind.Utc),
                    UserId = 1,
                }, new Collection()
                {
                    Id = 2,
                    Name = "Bosnian-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 14, 21, 49, 05, DateTimeKind.Utc),
                    UserId = 1,
                }, new Collection()
                {
                    Id = 3,
                    Name = "Math-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 14, 11, 49, 05, DateTimeKind.Utc),
                    UserId = 2,
                }, new Collection()
                {
                    Id = 4,
                    Name = "Bosnian-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 14, 13, 49, 05, DateTimeKind.Utc),
                    UserId = 2,
                }, new Collection()
                {
                    Id = 5,
                    Name = "Math-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 13, 11, 49, 05, DateTimeKind.Utc),
                    UserId = 3,
                }, new Collection()
                {
                    Id = 6,
                    Name = "Bosnian-Collection",
                    Description = "-",
                    CreatedAt = new DateTime(2025, 10, 13, 22, 49, 05, DateTimeKind.Utc),
                    UserId = 3,
                }
                );

        }
    }
}
