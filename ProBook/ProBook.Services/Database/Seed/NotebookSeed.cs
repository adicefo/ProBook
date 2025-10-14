using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class NotebookSeed
    {
        public static void SeedData(this EntityTypeBuilder<Notebook> entity)
        {
            entity.HasData(
               new Notebook()
               {
                   Id=1,
                   Name="Math-1",
                   Description="-",
                   ImageUrl=null,
                   CreatedAt = new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
                   UserId=1,
               },
               new Notebook()
               {
                   Id = 2,
                   Name = "Math-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 13, 20, 49, 05, DateTimeKind.Utc),
                   UserId = 1,
               },
               new Notebook()
               {
                   Id = 3,
                   Name = "Math-3",
                    Description="-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
                   UserId = 1,
               },
               new Notebook()
               {
                   Id = 4,
                   Name = "Bosnian-1",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 13, 21, 49, 05, DateTimeKind.Utc),
                   UserId = 1,
               },
               new Notebook()
               {
                   Id = 5,
                   Name = "Bosnian-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 13, 21, 00, 05, DateTimeKind.Utc),
                   UserId = 1,
               },
               new Notebook()
               {
                   Id = 6,
                   Name = "Math-1",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 19, 49, 05, DateTimeKind.Utc),
                   UserId = 2,
               },
               new Notebook()
               {
                   Id = 7,
                   Name = "Math-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 19, 49, 05, DateTimeKind.Utc),
                   UserId = 2,
               },
               new Notebook()
               {
                   Id = 8,
                   Name = "Math-3",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 19, 49, 05, DateTimeKind.Utc),
                   UserId = 2,
               },
               new Notebook()
               {
                   Id = 9,
                   Name = "Bosnian-1",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 11, 49, 05, DateTimeKind.Utc),
                   UserId = 2,
               },
               new Notebook()
               {
                   Id = 10,
                   Name = "Bosnian-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 23, 49, 05, DateTimeKind.Utc),
                   UserId = 2,
               },
               new Notebook()
               {
                   Id = 11,
                   Name = "Math-1",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 22, 49, 05, DateTimeKind.Utc),
                   UserId = 3,
               },
               new Notebook()
               {
                   Id = 12,
                   Name = "Math-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 22, 49, 05, DateTimeKind.Utc),
                   UserId = 3,
               },
               new Notebook()
               {
                   Id = 13,
                   Name = "Math-3",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 09, 13, 19, 11, 05, DateTimeKind.Utc),
                   UserId = 3,
               },
               new Notebook()
               {
                   Id = 14,
                   Name = "Bosnian-1",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 19, 55, 05, DateTimeKind.Utc),
                   UserId = 3,
               }, new Notebook()
               {
                   Id = 15,
                   Name = "Bosnian-2",
                   Description = "-",
                   ImageUrl = null,
                   CreatedAt = new DateTime(2025, 10, 14, 19, 24, 05, DateTimeKind.Utc),
                   UserId = 3,
               }

                );

        }
    }
}
