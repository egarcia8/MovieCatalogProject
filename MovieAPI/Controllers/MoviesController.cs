using Microsoft.AspNetCore.Mvc;
using MovieProject.Data;
using MovieProject.Models;

namespace MovieProject.Controllers
{
    [ApiController] //annotation to flag this as an apicontroller for the program to see
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public MoviesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IEnumerable<Movies> Movies()
        {
            var movies = _unitOfWork.MovieRepository.Get(null, null, "Ratings,MovieGenres.Genres");

            return movies;
        }


        /// <summary>
        /// Get a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns a single movie</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<Movies> GetMovie(int id)
        {
            var movie = _unitOfWork.MovieRepository.Get(movie => movie.MovieId == id, null, "Ratings,MovieGenres.Genres");

            return Ok(movie.FirstOrDefault());
        }

        /// <summary>
        /// Create a new movie item
        /// </summary>
        /// <param name="postMovie"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult PostMovies(MovieDto postMovie)
        {
            var movieGenres = new List<MovieGenres>();

            foreach (var mg in postMovie.MovieGenres)
            {
                var genre = _unitOfWork.GenreRepository.GetByID(mg.GenreId);
                movieGenres.Add(new MovieGenres { Genres = genre });
            }

            var tempMovie = new Movies()
            {
                Title = postMovie.Title,
                Description = postMovie.Description,
                RatingId = postMovie.RatingId,
                MovieGenres = movieGenres
            };

            _unitOfWork.MovieRepository.Insert(tempMovie);

            _unitOfWork.Save();

            return Accepted(tempMovie);
        }

        /// <summary>
        /// Update a movie item
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Movies(int movieId, MovieDto movie)
        {
            var movies = _unitOfWork.MovieRepository.Get(m => m.MovieId == movieId, null, "MovieGenres").FirstOrDefault();


            if (movies == null)
            {
                return Problem(statusCode: 400, detail: "Could not find object", title: "400 Error");
              
            }

            movies.MovieGenres = new List<MovieGenres>();

            _unitOfWork.MovieRepository.Update(movies);

            var movieGenres = new List<MovieGenres>();
            foreach (var mg in movie.MovieGenres)
            {
                var genre = _unitOfWork.GenreRepository.GetByID(mg.GenreId);
                movieGenres.Add(new MovieGenres { Genres = genre });
            }

            movies.Title = movie.Title;
            movies.Description = movie.Description;
            movies.RatingId = movie.RatingId;
            movies.MovieGenres = movieGenres;
           

            _unitOfWork.MovieRepository.Update(movies);
            _unitOfWork.Save();

            
            return Accepted();

        }

        [HttpDelete]
        public ActionResult Movies(int movieId)
        {
            Movies movies = _unitOfWork.MovieRepository.GetByID(movieId);
            if (movies == null)
            {
                return BadRequest("Could not find object");
            }

            _unitOfWork.MovieRepository.Delete(movies);
            _unitOfWork.Save();
            
            return Accepted();
        }
    }
}
