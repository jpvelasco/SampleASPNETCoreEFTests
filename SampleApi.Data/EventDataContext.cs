using Microsoft.EntityFrameworkCore;
using SampleApi.Data.Entities;

namespace SampleApi.Data
{
    public class EventDataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        
        public EventDataContext()
        { }

        public EventDataContext(DbContextOptions<EventDataContext> options)
        : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SampleApiDatabase;Trusted_Connection=True;");
            }
        }
    }
}
