using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLibrary.Models
{
    public class BorrowedBooksList
    {
        public int BorrowedBooksListId { get; set; }
        public int BookId { get; set; }
        [ForeignKey("Id")]
        public virtual Book Book { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Id")]
        public virtual User User { get; set; }

        [NotMapped]
        public ICollection<User> UserList { get; set; }
        [NotMapped]
        public ICollection<Book> BookList { get; set; }
        

    }
}
