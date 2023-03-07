using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string Genre { get; set; } = default!;

    }
}