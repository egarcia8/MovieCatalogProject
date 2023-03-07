﻿namespace MovieProject.Models
{
    public class MovieDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int RatingId { get; set; }
        //public int GenreId { get; set; }
        public ICollection<Genres> MovieGenres { get; set; }

    }
}
