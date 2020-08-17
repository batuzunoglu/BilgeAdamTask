using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamTask.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogContext(DbContextOptions<BlogContext> context) : base(context)
        {

        }
    }

}
