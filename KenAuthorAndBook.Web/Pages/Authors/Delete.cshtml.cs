using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Web.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public DeleteModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Author = await _authorService.GetByIdAsync(id);

            if (Author == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToPage("/Authors/Index");
        }
    }
}
