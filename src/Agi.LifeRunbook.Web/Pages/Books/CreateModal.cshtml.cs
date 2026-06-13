using System.Threading.Tasks;
using Agi.LifeRunbook.Books;
using Microsoft.AspNetCore.Mvc;

namespace Agi.LifeRunbook.Web.Pages.Books
{
    public class CreateModalModel : LifeRunbookPageModel
    {
        [BindProperty]
        public CreateUpdateBookDto Book { get; set; } = new();

        private readonly IBookAppService _bookAppService;

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public void OnGet()
        {
            Book = new CreateUpdateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}
