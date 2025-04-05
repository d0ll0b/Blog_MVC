using Microsoft.EntityFrameworkCore;
using Blog_MVC.Models;

namespace Blog_MVC.Data
{
	public class BlogContext : DbContext
    {
		public BlogContext()
		{
		}

        public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; } = default!;
    }
}


