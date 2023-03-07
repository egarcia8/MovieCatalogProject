using Microsoft.EntityFrameworkCore;
using MovieProject.Models;

namespace MovieProject.Data
{
    public class MovieDbContext : DbContext
    {
        //the name of the connection string (which is added to the Web.config file) is passed in to the constructor below
        //if you don't specify a connection string or the name of one explicitly, Entity Framework assumes the connection string name is the same as the class name.
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

        //DbSet property is created for each entity set, entity set is the table, an entity refers to the row in the table
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Genres> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }


}
