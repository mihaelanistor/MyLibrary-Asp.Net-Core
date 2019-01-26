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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Categories/GetAll
        [HttpGet]
        public string GetAll()
        {

            List<Category> categories = _context.Category.ToList();

            string json = JsonConvert.SerializeObject(categories);

            return json;
        }

        // GET: /Categories/GetById/123456
        [HttpGet]
        public string GetById(int id)
        {
            Category category = _context.Category.Where(b => b.Id == id).FirstOrDefault();

            string json = JsonConvert.SerializeObject(category);

            return json;
        }

        // GET: /Categories/Search?SearchedString=than
        [HttpGet]
        public string Search(string searchedString = null)
        {

            List<Category> categories = _context.Category.Where(b => b.Name.Contains(searchedString)).ToList();
            

            string json = JsonConvert.SerializeObject(categories);
            return json;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task Create([FromBody] Category category)
        {

            if (category == null)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();

            }
        }

        [HttpPost]
        public async Task Edit(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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


        // POST: Categories/Delete/5
        [HttpPost]
        public async Task Delete(int id)
        {
            var category = _context.Category.Where(b => b.Id == id).FirstOrDefault();
            if (category == null)
            {
                return;
            }
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
