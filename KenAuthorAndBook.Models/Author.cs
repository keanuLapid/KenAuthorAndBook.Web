using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Models
{
    public class Author
    {
        public Guid AuthorId { get; set; } 
        public string? Name { get; set; } 
        public string? Bio { get; set; } 
        public ICollection<Book> Books { get; set; } = new List<Book>(); 
    }
}
