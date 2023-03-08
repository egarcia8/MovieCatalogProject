using System.ComponentModel.DataAnnotations;

namespace MoviesList.Models
{
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }
        public string Rating { get; set; } = default!;
    }
}
