using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyLibrary.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //public Author()
        //{
        //    book = new HashSet<Book>();
        //}

        public ICollection<Book> Book { get; set; }

        //public Author(string firstName, string lastName) : base()
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //}
        //public Author() : base() { }
    }
        
}
