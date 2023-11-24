using Microsoft.EntityFrameworkCore;
using MoviesMvc.Models;

namespace MoviesMvc.Data;

public class MoviesDbContext: DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {
           
    }

    public DbSet<Movie> Movies { get; set; }
}
