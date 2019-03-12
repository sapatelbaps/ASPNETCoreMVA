namespace WebApplication2
{
    using Microsoft.EntityFrameworkCore;

    public class TicketItem
    {
        public long Id { get; set; }

        public string Concert { get; set; }

        public string Artist { get; set; }

        public bool IsAvailable { get; set; }
    }

    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) 
        :base(options)
        {
            
        }

        public DbSet<TicketItem> TicketItems { get; set; }

    }
}
