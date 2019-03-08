using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            this._db = db;
        }

        public IList<Customer> Customers { get; private set; }

        public async Task OnGetAsync()
        {
            Customers = await this._db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<ActionResult> OnPostDeleteAsync(int id)
        {
            Customer customer = await this._db.Customers.FindAsync(id);

            if (customer !=null)
            {
                this._db.Customers.Remove(customer);
                this._db.SaveChangesAsync();
            }

            return this.RedirectToPage();
        }

    }
}
