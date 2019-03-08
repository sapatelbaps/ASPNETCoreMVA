using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        private ILogger<CreateModel> Log;

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public CreateModel(AppDbContext db, ILogger<CreateModel> log)
        {
            this._db = db;
            this.Log = log;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            this._db.Customers.Add(Customer);
            await this._db.SaveChangesAsync();

            Message = $"Customer {Customer.Name} added!";

            this.Log.LogCritical(Message);

            return this.RedirectToPage("/Index");
        }
    }
}