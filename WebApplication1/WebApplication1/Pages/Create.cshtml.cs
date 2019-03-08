using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
       
        [BindProperty]
        public Customer Customer { get; set; }

        public CreateModel(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            this._db.Customers.Add(Customer);
            await this._db.SaveChangesAsync();

            return this.RedirectToPage("/Index");
        }
    }
}