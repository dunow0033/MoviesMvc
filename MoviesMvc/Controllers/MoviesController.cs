using Microsoft.AspNetCore.Mvc;

namespace MoviesMvc.Controllers;

public class MoviesController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
