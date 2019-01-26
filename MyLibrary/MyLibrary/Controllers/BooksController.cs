using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookRepository _bookService;

        public BooksController(ApplicationDbContext context, BookRepository bookService)
        {
            _context = context;
            _bookService = bookService;
        }


        // GET: Books
        public /*List<Book>*/ async Task<IActionResult> Index()
        {
            //List<Book> books = _bookService.GetAll();
            //return View(books);


            var books = _context.Book
                .Include(a => a.Author)
                .Include(c => c.Category)
                .Include(g => g.Genre)
                .Include(l => l.Language);

            return View(books.ToList());



            //return View(await _context.Book.ToListAsync());
        }

        // GET: Books/GetAll
        [HttpGet]
        public string GetAll() {
            
            List<Book> books = _context.Book.ToList();

            foreach (var book in books)
            {
                if (book != null )
                {
                    book.Author = _context.Author.Where(a => a.Id == book.AuthorId).FirstOrDefault();
                    book.Author.Book = null;

                    book.Category = _context.Category.Where(a => a.Id == book.CategoryId).FirstOrDefault();
                    book.Category.Book = null;

                    book.Language = _context.Language.Where(a => a.Id == book.LanguageId).FirstOrDefault();
                    book.Language.Book = null;

                    book.Genre = _context.Genre.Where(a => a.Id == book.GenreId).FirstOrDefault();
                    book.Genre.Book = null;
                }
            }
            
            string json = JsonConvert.SerializeObject(books.ToList());

            return json;
        }

        // GET: Books/GetById/7027
        [HttpGet]
        public string GetById(int id)
        {
            Book book = _context.Book.Where(b => b.Id == id).FirstOrDefault();

            book.Author = _context.Author.Where(a => a.Id == book.AuthorId).FirstOrDefault();
            book.Author.Book = null;

            book.Category = _context.Category.Where(a => a.Id == book.CategoryId).FirstOrDefault();
            book.Category.Book = null;

            book.Language = _context.Language.Where(a => a.Id == book.LanguageId).FirstOrDefault();
            book.Language.Book = null;

            book.Genre = _context.Genre.Where(a => a.Id == book.GenreId).FirstOrDefault();
            book.Genre.Book = null;

            string json = JsonConvert.SerializeObject(book);

            return json;
        }

        // GET: /Books/Search?searchedString=d
        [HttpGet]
        public string Search(string searchedString = null)
        {
         
            List<Book> books = _context.Book.Where(b => b.Title.Contains(searchedString)).ToList();

            foreach (var book in books)
            {
                if (book != null)
                {
                    book.Author = _context.Author.Where(a => a.Id == book.AuthorId).FirstOrDefault();
                    book.Author.Book = null;

                    book.Category = _context.Category.Where(a => a.Id == book.CategoryId).FirstOrDefault();
                    book.Category.Book = null;

                    book.Language = _context.Language.Where(a => a.Id == book.LanguageId).FirstOrDefault();
                    book.Language.Book = null;

                    book.Genre = _context.Genre.Where(a => a.Id == book.GenreId).FirstOrDefault();
                    book.Genre.Book = null;
                }
            }

            string json = JsonConvert.SerializeObject(books.ToList());
            return json;
        }

        // GET: Books/getByAuthorId/1
        [HttpGet]
        public string GetByAuthorId(int id)
        {
            List<Book> books = _context.Book.Where(b => b.AuthorId == id).ToList();

            foreach (var book in books)
            {
                if (book != null)
                {
                    book.Author = _context.Author.Where(a => a.Id == book.AuthorId).FirstOrDefault();
                    book.Author.Book = null;

                    book.Category = _context.Category.Where(a => a.Id == book.CategoryId).FirstOrDefault();
                    book.Category.Book = null;

                    book.Language = _context.Language.Where(a => a.Id == book.LanguageId).FirstOrDefault();
                    book.Language.Book = null;

                    book.Genre = _context.Genre.Where(a => a.Id == book.GenreId).FirstOrDefault();
                    book.Genre.Book = null;
                }
            }

            string json = JsonConvert.SerializeObject(books.ToList());
            return json;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task Create( [FromBody] Book book)
        {
 
            if (book == null)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();

            }
        }

 
        [HttpPost]
        public async Task Edit(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return;
                    }
                    else
                    {
                        throw;
                    }
                }        
            }
        }


        // POST: Books/Delete/5
        [HttpPost]
        public async Task Delete(int id)
        {
            var book = _context.Book.Where(b => b.Id == id).FirstOrDefault();
            if (book == null) {
                return;
            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
           
        }


        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        [HttpGet]
        public JObject IsLoggedIn()
        {

            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userId = User.Identity.IsAuthenticated.ToString();

            string str;
            if (userId == "True") {
                str = "{\"isLoggedIn\": true}";
            }
            else {
                str = "{\"isLoggedIn\": false}";
            }

            JObject json = JObject.Parse(str);
            return json;
        }

        }
    }
