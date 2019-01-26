using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using Newtonsoft.Json;


namespace MyLibrary.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: /Authors/GetAll
        [HttpGet]
        public string GetAll() {
            
            List<Author> authors = _context.Author.ToList();
                
            string json = JsonConvert.SerializeObject(authors);

            return json;
        }

        // GET: /Authors/GetById/123456
        [HttpGet]
        public string GetById(int id)
        {
            Author author = _context.Author.Where(b => b.Id == id).FirstOrDefault();
            
            string json = JsonConvert.SerializeObject(author);

            return json;
        }

        // GET: /Authors/Search?SearchedString=than
        [HttpGet]
        public string Search(string searchedString = null)
        {
         
            List<Author> authors = _context.Author.Where(b => b.FirstName.Contains(searchedString)).ToList();
            authors.AddRange(_context.Author.Where(b => b.LastName.Contains(searchedString)).ToList());
            
            string json = JsonConvert.SerializeObject(authors);
            return json;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task Create( [FromBody] Author author)
        {
 
            if (author == null)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();

            }
        }

        [HttpPost]
        public async Task Edit(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            var author = _context.Author.Where(b => b.Id == id).FirstOrDefault();
            if (author == null) {
                return;
            }
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
        }



        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
