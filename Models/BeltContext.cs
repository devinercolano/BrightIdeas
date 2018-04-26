using Microsoft.EntityFrameworkCore;
 
namespace Belt.Models
{
    public class BeltContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas {get;set;}
        public DbSet<Like> Likes {get; set;} 
    }
}