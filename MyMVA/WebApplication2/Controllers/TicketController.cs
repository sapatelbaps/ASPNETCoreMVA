using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    using Microsoft.EntityFrameworkCore;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext _context;

        public TicketController(TicketContext context)
        {
            this._context = context;

            if (this._context.TicketItems.Count() == 0)
            {
                this._context.TicketItems.Add(new TicketItem() { Concert = "Shakira" });
                this._context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Test()
        {
            return new ObjectResult("SwamiShriji");
        }

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return this._context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            var ticket = this._context.TicketItems.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return this.NotFound(); //404
            }

            return new ObjectResult(ticket);

        }

        [HttpPost]
        public IActionResult Create([FromBody]TicketItem ticket)
        {
            if (ticket == null)
            {
                return this.BadRequest();
            }

            this._context.TicketItems.Add(ticket);
            this._context.SaveChanges();

            // return "/Ticket"/ + ticket.Id       (also add status code in header.)
            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);

        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TicketItem ticket)
        {
            if (ticket == null || ticket.Id != id)
            {
                return this.BadRequest();
            }

            var tic = this._context.TicketItems.FirstOrDefault(t => t.Id == id);

            if (tic == null)
            {
                return this.NotFound();
            }

            tic.Concert = ticket.Concert;
            tic.Artist = ticket.Artist;
            tic.IsAvailable = ticket.IsAvailable;

            this._context.TicketItems.Update(tic);
            this._context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tics = this._context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (tics == null)
            {
                return this.NotFound();
            }

            this._context.TicketItems.Remove(tics);
            this._context.SaveChanges();

            return new NoContentResult();
        }

    }
}