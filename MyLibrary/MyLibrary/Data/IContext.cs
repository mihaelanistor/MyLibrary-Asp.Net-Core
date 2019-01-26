using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Data
{
    public class IContext : IdentityDbContext
    {
        public const string connectionString = "Server=(localdb)\\mssgllocaldb;Database=aspnet-MyLibrary-FCDFC560-51E0-402F-95A3-2A24E9837FD2;Trusted_Connection=true";

        public IContext()
        {
        }

      
        public IContext(DbContextOptions<IContext> options)
               : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BorrowedBooksList> BorrowedBooksList { get; set; }
    }
}
