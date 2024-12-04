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
    public class UpdateModel : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public UpdateModel(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        [BindProperty]
        public UpdateAuthorViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            Input = new UpdateAuthorViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Bio = author.Bio,
                BookIds = author.Books.Select(b => b.BookId).ToList()
            };

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
                AuthorId = Input.AuthorId,
                Name = Input.Name,
                Bio = Input.Bio,
                Books = Input.BookIds?.Select(bookId => new Book { BookId = bookId }).ToList()
            };

            await _authorService.UpdateAsync(author);
            return RedirectToPage("/Authors/Index");
        }
    }

    public class UpdateAuthorViewModel
    {
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public IEnumerable<Guid>? BookIds { get; set; }
    }
}
