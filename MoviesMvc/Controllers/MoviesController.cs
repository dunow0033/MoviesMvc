using Microsoft.AspNetCore.Mvc;
using MoviesMvc.Models;
using MoviesMvc.Data;

namespace MoviesMvc.Controllers;

public class MoviesController : Controller
{
    private readonly MoviesDbContext _context;

    public MoviesController(MoviesDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Movie movie)
    {
        var newMovie = new Movie()
        {
            Title = movie.Title,
            Release_date = movie.Release_date,
            Genre = movie.Genre,
            Price = movie.Price,
            Rating = movie.Rating,
        };

        _context.Movies.Add(newMovie);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}