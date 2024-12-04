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
        
        public IndexModel(ILogger<IndexModel> logger, IAuthorService authorService, IBookService bookService)
        {
            _logger = logger;
            
        }

        public async Task OnGetAsync()
        {
            
        }
    }
}
