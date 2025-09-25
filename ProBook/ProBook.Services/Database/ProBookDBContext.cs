using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class ProBookDBContext:DbContext
    {
        public ProBookDBContext(DbContextOptions<ProBookDBContext> options)
           : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<NotebookCollection> NotebookCollections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SharedNotebook> SharedNotebooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Further configuration if needed

           


            modelBuilder.Entity<SharedNotebook>()
        .HasOne(sn => sn.FromUser) 
        .WithMany() 
        .HasForeignKey(sn => sn.FromUserId);

            modelBuilder.Entity<SharedNotebook>()
     .HasOne(sn => sn.ToUser)
     .WithMany()
     .HasForeignKey(sn => sn.ToUserId);

        }
    }
}
