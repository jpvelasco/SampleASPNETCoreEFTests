using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SampleApi.Data
{
    public class EventDbContextFactory : IDbContextFactory<EventDataContext>
    {
        public EventDataContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<EventDataContext>();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SampleApiDatabase;Trusted_Connection=True;");
            return new EventDataContext(builder.Options);
        }
    }
}
