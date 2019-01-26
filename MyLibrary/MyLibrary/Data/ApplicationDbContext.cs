using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public const string connectionString = "Server=(localdb)\\mssgllocaldb;Database=test2;Trusted_Connection=true;MultipleActiveResultSets=true";

        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.AuthorId).IsRequired();
            });

            modelBuilder.Entity<Book>()
                .HasKey(a => new { a.Id });

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Book)
                .HasForeignKey(a => a.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Category)
                .WithMany(b => b.Book)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Language)
                .WithMany(b => b.Book)
                .HasForeignKey(a => a.LanguageId);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Genre)
                .WithMany(b => b.Book)
                .HasForeignKey(a => a.GenreId);

           
            var author = new Author
            {
                Id = 10301,
                FirstName = "Lev",
                LastName = "Grossman"
            };
            var author2 = new Author
            {
                Id = 23001,
                FirstName = "Jonathan",
                LastName = "Stroud"
            };

            var genre = new Genre()
            {
                Id = 123001,
                Name = "fantasy"
            };
            var language = new Language()
            {
                Id = 102301,
                Name = "english"
            };
            var category = new Category()
            {
                Id = 12030121,
                Name = "young adult"
            };
            var category2 = new Category()
            {
                Id = 23400654,
                Name = "children"
            };
            var book1 = new Book
            {
                Id = 1231023,
                Title = "The Magicians",
                CategoryId = category.Id,
                GenreId = genre.Id,
                LanguageId = language.Id,
                ReleaseYear = 2009,
                AuthorId = author.Id
            };
            var book2 = new Book()
            {
                Id = 234234,
                Title = "The Magician King",
                CategoryId = category.Id,
                GenreId = genre.Id,
                LanguageId = language.Id,
                AuthorId = author.Id,
                ReleaseYear = 2011
            };
            var book3 = new Book()
            {
                Id = 345345,
                Title = "The Magician's Land",
                CategoryId = category.Id,
                GenreId = genre.Id,
                LanguageId = language.Id,
                AuthorId = author.Id,
                ReleaseYear = 2014
            };
            var book4 = new Book()
            {
                Id = 4564506,
                Title = "The Amulet of Samarkand",
                CategoryId = category2.Id,
                GenreId = genre.Id,
                LanguageId = language.Id,
                AuthorId = author2.Id,
                ReleaseYear = 2003
            };

            modelBuilder.Entity<Author>().HasData(author, author2);
            modelBuilder.Entity<Genre>().HasData(genre);
            modelBuilder.Entity<Category>().HasData(category, category2);
            modelBuilder.Entity<Language>().HasData(language);
            
            modelBuilder.Entity<Book>().HasData(book1,book2, book3, book4);

            //modelBuilder.Entity<Genre>().HasData(new Genre()
            //{
            //    Id = 0,
            //    Name = "fantasy"
            //});

            //modelBuilder.Entity<Language>().HasData(new Language()
            //{
            //    Id = 0,
            //    Name = "english"
            //});
            //modelBuilder.Entity<Language>().HasData(new Language()
            //{
            //    Id = 1,
            //    Name = "french"
            //});

            //modelBuilder.Entity<Category>().HasData(new Category()
            //{
            //    Id = 0,
            //    Name = "teens"
            //});
            //modelBuilder.Entity<Category>().HasData(new Category()
            //{
            //    Id = 1,
            //    Name = "young adult"
            //});

            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 1,
            //    Title = "The Magicians",
            //    AuthorId = 1,
            //    //Author.AuthorId = 0,
            //    //Id = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2009
            //});

            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 2,
            //    Title = "The Magician King",
            //    AuthorId = 1,
            //    //Id = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2011
            //});
            //modelBuilder.Entity<Book>().HasData(new Book()
            //{
            //    Id = 3,
            //    Title = "The Magician's Land",
            //    AuthorId = 1,
            //    //Id = 1,
            //    //GenreId = 0,
            //    //LanguageId = 0,
            //    ReleaseYear = 2014
            //});
        }
    }
}