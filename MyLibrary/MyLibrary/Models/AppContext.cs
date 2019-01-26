using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary
{
    class AppContext : DbContext
    {
        public const string connectionString = "Server=(localdb)\\mssgllocaldb;Database=aspnet-MyLibrary-FCDFC560-51E0-402F-95A3-2A24E9837FD2;Trusted_Connection=true";

        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(connectionString);
            }
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>(entity =>
            //{
            //    entity.Property(e => e.Url).IsRequired();
            //});

            //modelBuilder.Entity<Book>(entity =>
            //{
            //    entity.HasOne(d => d.Author)
            //        .WithMany(p => p.Book)
            //        .HasForeignKey(d => d.AuthorId);
            //});

            //modelBuilder.Entity<Author>().HasData(new Author()
            //{
            //    AuthorId = 0,
            //    FirstName = "Lev",
            //    LastName = "Grossman"
            //});

            //modelBuilder.Entity<Genre>().HasData(new Genre()
            //{
            //    GenreId = 0,
            //    Name = "fantasy"
            //});

            //modelBuilder.Entity<Language>().HasData(new Language()
            //{
            //    LanguageId = 0,
            //    Name = "english"
            //});
            //modelBuilder.Entity<Language>().HasData(new Language()
            //{
            //    LanguageId = 1,
            //    Name = "french"
            //});

            //modelBuilder.Entity<Category>().HasData(new Category()
            //{
            //    CategoryId = 0,
            //    Name = "teens"
            //});
            //modelBuilder.Entity<Category>().HasData(new Category()
            //{
            //    CategoryId = 1,
            //    Name = "young adult"
            //});

            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 0,
            //    Title = "The Magicians",
            //    AuthorId = 0,
            //    //Author.AuthorId = 0,
            //    //CategoryId = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2009
            //});


            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 0,
            //    Title = "The Magician King",
            //    AuthorId = 0,
            //    //CategoryId = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2011
            //});
            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 0,
            //    Title = "The Magician's Land",
            //    AuthorId = 0,
            //    //CategoryId = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2014
            //});

            //var author = new Author
            //{
           
            //    FirstName = "Lev",
            //    LastName = "Grossman"
            //};

            //var book1 = new Book
            //{
            //    Id = 0,
            //    Title = "The Magicians",
            //    //AuthorId = 0,
            //    //Author.AuthorId = 0,
            //    //CategoryId = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2009,
            //    Author = author
            //};

            //Books.Add(book1);
            //SaveChanges();


        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(connectionString);
        //}
    }
}
