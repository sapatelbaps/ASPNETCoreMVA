using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    using Microsoft.EntityFrameworkCore;

    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public Customer Customer { get; set; }

        public EditModel(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            Customer = await this._db.Customers.FindAsync(id);

            if (Customer == null)
            {
                return this.RedirectToPage("./Index");
            }

            return this.Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            this._db.Attach(Customer).State = EntityState.Modified;

            try
            {
                await this._db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Customer {Customer.Id} not found!",e);
            }

            return this.RedirectToPage("./Index");

        }

    }
}