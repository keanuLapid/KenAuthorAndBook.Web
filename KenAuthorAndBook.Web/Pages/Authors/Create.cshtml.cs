using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Web.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        [BindProperty]
        public CreateAuthorViewModel Input { get; set; }

        public CreateModel(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGet()
        {
            var books = await _bookService.GetAllAsync();
            ViewData["Books"] = books;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var books = await _bookService.GetAllAsync();
                ViewData["Books"] = books;
                return Page();
            }

            var author = new Author
            {
                AuthorId = Guid.NewGuid(),
                Name = Input.Name,
                Bio = Input.Bio,
                Books = Input.BookIds?.Select(bookId => new Book { BookId = bookId }).ToList()
            };

            await _authorService.AddAsync(author);
            return RedirectToPage("/Authors/Index");
        }
    }

    public class CreateAuthorViewModel
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public IEnumerable<Guid>? BookIds { get; set; }
    }
}
