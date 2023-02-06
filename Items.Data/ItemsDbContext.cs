using Items.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Items.Data
{
    public class ItemsDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Color> Colors { get; set; }

        public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options)
        {

        }
    }
}
