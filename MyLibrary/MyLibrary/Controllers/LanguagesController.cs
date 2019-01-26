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
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Languages/GetAll
        [HttpGet]
        public string GetAll()
        {

            List<Language> languages = _context.Language.ToList();

            string json = JsonConvert.SerializeObject(languages);

            return json;
        }

        // GET: /Languages/GetById/123456
        [HttpGet]
        public string GetById(int id)
        {
            Language language = _context.Language.Where(b => b.Id == id).FirstOrDefault();

            string json = JsonConvert.SerializeObject(language);

            return json;
        }

        // GET: /Genres/Search?SearchedString=than
        [HttpGet]
        public string Search(string searchedString = null)
        {

            List<Language> languages = _context.Language.Where(b => b.Name.Contains(searchedString)).ToList();


            string json = JsonConvert.SerializeObject(languages);
            return json;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task Create([FromBody] Language language)
        {

            if (language == null)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();

            }
        }

        [HttpPost]
        public async Task Edit(int id, [FromBody] Language language)
        {
            if (id != language.Id)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
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
            var language = _context.Language.Where(b => b.Id == id).FirstOrDefault();
            if (language == null)
            {
                return;
            }
            _context.Language.Remove(language);
            await _context.SaveChangesAsync();
        }

        private bool LanguageExists(int id)
        {
            return _context.Language.Any(e => e.Id == id);
        }
    }
}
