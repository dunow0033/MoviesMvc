using Microsoft.AspNetCore.Mvc;
using MoviesMvc.Models;
using MoviesMvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesMvc.Movies.Models;

namespace MoviesMvc.Controllers;

public class MoviesController : Controller
{
    private readonly MoviesDbContext _context;

    public MoviesController(MoviesDbContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    var movies = await _context.Movies.ToListAsync();
    //    return View(movies);
    //}

	[HttpGet]
	public async Task<IActionResult> Index(string movieGenre, string searchString)
	{
		// Use LINQ to get list of genres.
		IQueryable<string> genreQuery = from m in _context.Movies
										select m.Genre;
		var movies = from m in _context.Movies
					 select m;

		if (!string.IsNullOrEmpty(searchString))
		{
			movies = movies.Where(s => s.Title!.Contains(searchString));
		}

		if (!string.IsNullOrEmpty(movieGenre))
		{
			movies = movies.Where(x => x.Genre == movieGenre);
		}

		var movieGenreVM = new MovieGenreViewModel
		{
			Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
			Movies = await movies.ToListAsync()
		};

		return View(movieGenreVM);
	}



	[HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Detail(Guid id)
    {
		if (id == null || _context.Movies == null)
		{
			return NotFound();
		}

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

    public async Task<IActionResult> Edit(Guid? id)
    {
		if (id == null || _context.Movies == null)
		{
			return NotFound();
		}

		var movie = await _context.Movies.FindAsync(id);

		if (movie == null)
		{
			return NotFound();
		}
		return View(movie);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, Movie movie)
	{
		if(id != movie.Id)
        {
            return NotFound();
        }

        var findMovie = await _context.Movies.FindAsync(movie.Id);

		if(findMovie != null)
		{
            findMovie.Title = movie.Title;
            findMovie.Release_date = movie.Release_date;
            findMovie.Genre = movie.Genre;
            findMovie.Price = movie.Price;
            findMovie.Rating = movie.Rating;

			await _context.SaveChangesAsync();
		};

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid? id, Movie movie)
	{
		if (id == null || _context.Movies == null)
		{
			return NotFound();
		}

		var findMovie = await _context.Movies.FindAsync(movie.Id);

		if (findMovie != null)
		{
			return View(findMovie);
		}
		else
			return NotFound();
	}


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid? id, Movie movie)
    {
		if (id != movie.Id)
		{
			return NotFound();
		}

		var findMovie = await _context.Movies.FindAsync(movie.Id);

		if (findMovie != null)
		{
			findMovie.Title = movie.Title;
			findMovie.Release_date = movie.Release_date;
			findMovie.Genre = movie.Genre;
			findMovie.Price = movie.Price;
			findMovie.Rating = movie.Rating;

			_context.Movies.Remove(findMovie);

			await _context.SaveChangesAsync();
		};

		return RedirectToAction("Index");
	}

		private bool MovieExists(Guid id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}
}