using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MyLibrary.Data
{
    public class AuthorsRepository
    {
        public IEnumerable<SelectListItem> GetAuthors()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> authors = context.Author.AsNoTracking()
                    .OrderBy(n => n.FirstName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.FirstName + " " + n.LastName
                        }).ToList();
                var author = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select country ---"
                };
                
                authors.Insert(0, author);
                return new SelectList(authors, "Value", "Text");
            }
        }
    }
}
