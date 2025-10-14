using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class NotebookCollectionSeed
    {
        public static void SeedData(this EntityTypeBuilder<NotebookCollection> entity)
        {
            entity.HasData(
                new NotebookCollection()
                {
                    Id=1,
                    CreatedAt = new DateTime(2025, 10, 11, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId=1,
                    NotebookId=1
                }, new NotebookCollection()
                {
                    Id = 2,
                    CreatedAt = new DateTime(2025, 10, 11, 11, 49, 05, DateTimeKind.Utc),
                    CollectionId = 1,
                    NotebookId = 2
                }, new NotebookCollection()
                {
                    Id = 3,
                    CreatedAt = new DateTime(2025, 10, 11, 10, 49, 05, DateTimeKind.Utc),
                    CollectionId = 1,
                    NotebookId = 3
                }, new NotebookCollection()
                {
                    Id = 4,
                    CreatedAt = new DateTime(2025, 10, 10, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 2,
                    NotebookId = 4
                }, new NotebookCollection()
                {
                    Id = 5,
                    CreatedAt = new DateTime(2025, 10, 9, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 2,
                    NotebookId = 5
                }, new NotebookCollection()
                {
                    Id = 6,
                    CreatedAt = new DateTime(2025, 10, 9, 12, 49, 05, DateTimeKind.Utc),
                    CollectionId = 3,
                    NotebookId = 6
                }, new NotebookCollection()
                {
                    Id = 7,
                    CreatedAt = new DateTime(2025, 10, 6, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 3,
                    NotebookId = 7
                }, new NotebookCollection()
                {
                    Id = 8,
                    CreatedAt = new DateTime(2025, 10, 5, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 3,
                    NotebookId = 8
                }, new NotebookCollection()
                {
                    Id = 9,
                    CreatedAt = new DateTime(2025, 10, 11, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 4,
                    NotebookId = 9
                }, new NotebookCollection()
                {
                    Id = 10,
                    CreatedAt = new DateTime(2025, 10, 1, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 4,
                    NotebookId = 10
                }, new NotebookCollection()
                {
                    Id = 11,
                    CreatedAt = new DateTime(2025, 10, 2, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 5,
                    NotebookId = 11
                }, new NotebookCollection()
                {
                    Id = 12,
                    CreatedAt = new DateTime(2025, 10, 4, 22, 49, 05, DateTimeKind.Utc),
                    CollectionId = 5,
                    NotebookId = 12
                }, new NotebookCollection()
                {
                    Id = 13,
                    CreatedAt = new DateTime(2025, 6, 11, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 5,
                    NotebookId = 13
                }, new NotebookCollection()
                {
                    Id = 14,
                    CreatedAt = new DateTime(2025, 9, 11, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 6,
                    NotebookId = 14
                }, new NotebookCollection()
                {
                    Id = 15,
                    CreatedAt = new DateTime(2025, 10, 6, 19, 49, 05, DateTimeKind.Utc),
                    CollectionId = 6,
                    NotebookId = 15
                }
                );

        }
    }
}
