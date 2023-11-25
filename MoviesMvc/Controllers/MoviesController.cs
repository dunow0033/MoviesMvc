using Microsoft.AspNetCore.Mvc;
using MoviesMvc.Models;
using MoviesMvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MoviesMvc.Controllers;

public class MoviesController : Controller
{
    private readonly MoviesDbContext _context;

    public MoviesController(MoviesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Detail(Guid id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        var newMovie = new Movie()
        {
            Title = movie.Title,
            Release_date = movie.Release_date,
            Genre = movie.Genre,
            Price = movie.Price,
            Rating = movie.Rating,
        };

        await _context.Movies.AddAsync(newMovie);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}