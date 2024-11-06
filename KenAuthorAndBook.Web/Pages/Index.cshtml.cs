using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Book> Books { get; set; } = new List<Book>();

        public IndexModel(ILogger<IndexModel> logger, IAuthorService authorService, IBookService bookService)
        {
            _logger = logger;
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task OnGetAsync()
        {
            Authors = (await _authorService.GetAllAsync()).ToList();
            Books = (await _bookService.GetAllAsync()).ToList();
        }
    }
}
