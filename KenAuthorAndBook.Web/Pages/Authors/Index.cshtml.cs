using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.EntityFramework;
using KenAuthorAndBook.Services;
using KenAuthorAndBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace KenAuthorAndBook.Web.Pages.Authors
{
    public class Index : PageModel
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public List<Author> Authors { get; set; } = new List<Author>();

        [BindProperty]
        public SearchParameters? SearchParams { get; set; }

        public Index(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task OnGet(string? keyword = "", string? searchBy = "", string? sortBy = null, string? sortAsc = "true", int pageSize = 5, int pageIndex = 1)
        {
            if (SearchParams == null)
            {
                SearchParams = new SearchParameters()
                {
                    SortBy = sortBy,
                    SortAsc = sortAsc == "true",
                    SearchBy = searchBy,
                    Keyword = keyword,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }

            // Fetching authors without using Include (but will manually load books later)
            var authors = (await _authorService.GetAllAsync()).ToList();

            // Manually load related books for each author
            var books = await _bookService.GetAllAsync(); // Fetch all books at once
            foreach (var author in authors)
            {
                author.Books = books.Where(b => b.AuthorId == author.AuthorId).ToList(); // Associate books with the author
            }

            // Filtering by search parameters
            if (!string.IsNullOrEmpty(SearchParams.SearchBy) && !string.IsNullOrEmpty(SearchParams.Keyword))
            {
                if (SearchParams.SearchBy.ToLower() == "name")
                {
                    authors = authors.Where(a => a.Name?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
                }
                else if (SearchParams.SearchBy.ToLower() == "bio")
                {
                    authors = authors.Where(a => a.Bio?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
                }
                else if (SearchParams.SearchBy.ToLower() == "book")
                {
                    authors = authors.Where(a => a.Books?.Any(b => b.Title?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true) == true).ToList();
                }
            }
            else if (string.IsNullOrEmpty(SearchParams.SearchBy) && !string.IsNullOrEmpty(SearchParams.Keyword))
            {
                authors = authors.Where(a => a.Name?.ToLower().Contains(SearchParams.Keyword.ToLower()) == true).ToList();
            }

            // Sorting authors based on selected field
            if (SearchParams.SortBy == null || SearchParams.SortAsc == null)
            {
                Authors = authors;
                goto page;
            }

            if (SearchParams.SortBy.ToLower() == "name" && SearchParams.SortAsc == true)
            {
                Authors = authors.OrderBy(a => a.Name).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "name" && SearchParams.SortAsc == false)
            {
                Authors = authors.OrderByDescending(a => a.Name).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "bio" && SearchParams.SortAsc == true)
            {
                Authors = authors.OrderBy(a => a.Bio).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "bio" && SearchParams.SortAsc == false)
            {
                Authors = authors.OrderByDescending(a => a.Bio).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "book" && SearchParams.SortAsc == true)
            {
                Authors = authors.OrderBy(a => a.Books?.FirstOrDefault()?.Title).ToList();
            }
            else if (SearchParams.SortBy.ToLower() == "book" && SearchParams.SortAsc == false)
            {
                Authors = authors.OrderByDescending(a => a.Books?.FirstOrDefault()?.Title).ToList();
            }
            else
            {
                Authors = authors;
            }

        page:
            // Paging logic
            int totalPages = (int)Math.Ceiling((double)Authors.Count / SearchParams.PageSize.Value);
            int skip = (SearchParams.PageIndex.Value - 1) * SearchParams.PageSize.Value;
            Authors = Authors.Skip(skip).Take(SearchParams.PageSize.Value).ToList();
            SearchParams.PageCount = totalPages;
        }



        public class SearchParameters
        {
            public string? SearchBy { get; set; }
            public string? Keyword { get; set; }
            public string? SortBy { get; set; }
            public bool? SortAsc { get; set; }
            public int? PageSize { get; set; }
            public int? PageIndex { get; set; }
            public int? PageCount { get; set; }
        }
    }
}
