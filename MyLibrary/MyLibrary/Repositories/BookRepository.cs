using System.Collections.Generic;
using System.Linq;
using MyLibrary.Data;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Insert(Book obj)
        {
            _context.Book.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Book obj)
        {
            //var postFromDb = _context.Posts.FirstOrDefault(u => u.Id == obj.Id);
            //postFromDb = obj;

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Book obj)
        {
            _context.Book.Remove(obj);
            _context.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _context.Book.ToList();
        }

        public Book FindById(int id)
        {
            return _context.Book.FirstOrDefault(u => u.Id == id);
        }

        public List<Book> GetAllPostsFromBlog(int bookId)
        {
            return _context.Book.Where(u => u.Id == bookId).ToList();
        }
    }
}