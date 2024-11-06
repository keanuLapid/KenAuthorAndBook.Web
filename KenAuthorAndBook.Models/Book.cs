using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Models
{
    public class Book
    {
        public Guid BookId { get; set; } 
        public string? Title { get; set; } 
        public DateTime PublicationDate { get; set; } 
        public Guid AuthorId { get; set; } 
        public Author? Author { get; set; } 
    }
}
