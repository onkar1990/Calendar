using Calendar.Api.Entities;
using Microsoft.EntityFrameworkCore;


namespace Calendar.Api.DbContexts
{
    public class CalendarEventContext : DbContext
    {
        public CalendarEventContext(DbContextOptions<CalendarEventContext> options)
           : base(options)
        {
        }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            base.OnModelCreating(modelBuilder);
        }
    }
}
