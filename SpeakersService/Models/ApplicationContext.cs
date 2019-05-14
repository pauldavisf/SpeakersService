using System.IO;
using Microsoft.EntityFrameworkCore;

namespace SpeakersService.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Speaker> Files { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
