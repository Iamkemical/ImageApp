using ImageApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {
            
        }
        public DbSet<ImageServerModel> ImageServer { get; set; }
        public DbSet<ImageDatabaseModel> ImageDatabase { get; set; }
    }
}