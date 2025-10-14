using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class SharedNotebookSeed
    {
        public static void SeedData(this EntityTypeBuilder<SharedNotebook> entity)
        {
            entity.HasData(
               new SharedNotebook()
               {
                   Id=1,
                   FromUserId=1,
                   NotebookId=1,
                   ToUserId=2,
                   IsForEdit=false,
                   SharedDate = new DateTime(2025, 10, 13, 19, 49, 05, DateTimeKind.Utc),
               }, new SharedNotebook()
               {
                   Id = 2,
                   FromUserId = 3,
                   NotebookId = 14,
                   ToUserId = 2,
                   IsForEdit = true,
                   SharedDate = new DateTime(2025, 10, 13, 11, 49, 05, DateTimeKind.Utc),
               }, new SharedNotebook()
               {
                   Id = 3,
                   FromUserId = 2,
                   NotebookId = 7,
                   ToUserId = 3,
                   IsForEdit = false,
                   SharedDate = new DateTime(2025, 10, 11, 19, 49, 05, DateTimeKind.Utc),
               }, new SharedNotebook()
               {
                   Id = 4,
                   FromUserId = 1,
                   NotebookId = 4,
                   ToUserId = 3,
                   IsForEdit = true,
                   SharedDate = new DateTime(2025, 10, 10, 11, 49, 05, DateTimeKind.Utc),
               }, new SharedNotebook()
               {
                   Id = 5,
                   FromUserId = 3,
                   NotebookId = 13,
                   ToUserId = 2,
                   IsForEdit = false,
                   SharedDate = new DateTime(2025, 10, 9, 10, 49, 05, DateTimeKind.Utc),
               }
                );

        }
    }
}
