using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //[Range(1800, 2019)]
        [Display(Name = "Release year")]
        public int ReleaseYear { get; set; }
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        //public int AuthorId { get; set; }
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //public Author Author { get; set; }
        //[Required]
        [Display(Name = "Author Name")]
        //[ForeignKey("Id")]
        public int AuthorId { get; set; }
        [ForeignKey("Id")]
        public virtual Author Author { get; set; }

        [NotMapped]
        public ICollection<BorrowedBooksList> BorrowedBooksList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> AuthorList { get; set; }




        //public virtual Author Author { get; set; }
    }
}
