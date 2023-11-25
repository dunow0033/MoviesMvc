using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MoviesMvc.Models;

public class Movie
{
	public Guid Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }

    public string Release_date { get; set; }

    public string Genre { get; set; }
    public double Price { get; set; }
    public string Rating { get; set; }
}
