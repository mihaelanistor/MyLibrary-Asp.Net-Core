using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using Newtonsoft.Json;

namespace MyLibrary.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Genres/GetAll
        [HttpGet]
        public string GetAll()
        {

            List<Genre> genres = _context.Genre.ToList();

            string json = JsonConvert.SerializeObject(genres);

            return json;
        }

        // GET: /Genres/GetById/123456
        [HttpGet]
        public string GetById(int id)
        {
            Genre genre = _context.Genre.Where(b => b.Id == id).FirstOrDefault();

            string json = JsonConvert.SerializeObject(genre);

            return json;
        }

        // GET: /Genres/Search?SearchedString=than
        [HttpGet]
        public string Search(string searchedString = null)
        {

            List<Genre> genres = _context.Genre.Where(b => b.Name.Contains(searchedString)).ToList();


            string json = JsonConvert.SerializeObject(genres);
            return json;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task Create([FromBody] Genre genre)
        {

            if (genre == null)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();

            }
        }

        [HttpPost]
        public async Task Edit(int id, [FromBody] Genre genre)
        {
            if (id != genre.Id)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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


        // POST: Genres/Delete/5
        [HttpPost]
        public async Task Delete(int id)
        {
            var genre = _context.Genre.Where(b => b.Id == id).FirstOrDefault();
            if (genre == null)
            {
                return;
            }
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
        }

        private bool GenreExists(int id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }
}
