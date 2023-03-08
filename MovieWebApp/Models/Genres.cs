using System.ComponentModel.DataAnnotations;

namespace MoviesList.Models
{
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string Genre { get; set; } = default!;
    }
}

