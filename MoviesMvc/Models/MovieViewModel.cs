namespace MoviesMvc.Models;

public class MovieViewModel
{
	public Guid Id { get; set; }

	public string Title { get; set; }

	public string Release_date { get; set; }

	public string Genre { get; set; }
	public double Price { get; set; }
	public string Rating { get; set; }
}
