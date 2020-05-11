using Microsoft.EntityFrameworkCore;
using TravelHelper.Core.Domain;

namespace TravelHelper.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users {get;set;}
    }
}