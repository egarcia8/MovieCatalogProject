using Microsoft.EntityFrameworkCore;

namespace MovieProject.Models
{
    //use data annotations to declare composite key
    [PrimaryKey(nameof(MovieId), nameof(GenreId))]
    public class MovieGenres
    {
        public int MovieId { get; set; }
        //public Movies Movies { get; set; }

        public int GenreId { get; set; }
        public Genres Genres { get; set; } = default!;
    }
}

