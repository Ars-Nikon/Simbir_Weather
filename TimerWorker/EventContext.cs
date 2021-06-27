using Microsoft.EntityFrameworkCore;

namespace TimerWorker
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public EventContext(DbContextOptions<EventContext> options)
                 : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
