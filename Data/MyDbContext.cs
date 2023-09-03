using Auth.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
