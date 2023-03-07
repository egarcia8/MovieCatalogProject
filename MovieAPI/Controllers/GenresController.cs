using Microsoft.AspNetCore.Mvc;
using MovieProject.Data;
using MovieProject.Models;


namespace MovieProject.Controllers
{
    [ApiController] //annotation to flag this as an apicontroller for the program to see
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public GenresController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get a list of genre items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Genres> Genres()
        {
            var genres = _unitOfWork.GenreRepository.Get();
            return genres;
        }


        /// <summary>
        /// Get a genre by id
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <response code="200">Returns the list of items</response>
        [HttpGet("{id}")]
        public ActionResult GetGenre(int genreId)
        {
            Genres genre = _unitOfWork.GenreRepository.GetByID(genreId);

            if (genre == null)
            {
                return NotFound("That object is not found.");
            }

            return Accepted(genre);

        }

        /// <summary>
        /// Create a new genre item
        /// </summary>
        /// <param name="postGenre"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult PostGenres(GenreDto postGenre)
        {
            var tempGenre = new Genres()
            {
                GenreId = postGenre.GenreId,
                Genre = postGenre.Genre
            };
            _unitOfWork.GenreRepository.Insert(tempGenre);
            _unitOfWork.Save();

           
            return Accepted(tempGenre);
        }

        /// <summary>
        /// Delete a genre item by id
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Genres(int genreId)
        {
            Genres genres = _unitOfWork.GenreRepository.GetByID(genreId);


            var movies = _unitOfWork.MovieRepository.Get(m => m.MovieGenres.Select(g => g.GenreId == genreId).FirstOrDefault(), null, "MovieGenres.Genres");

            if (movies.ToList().Count > 0)
            {
                return Problem(statusCode: 400, detail: "Cannot delete genre; other table has dependency on it", title: "400 Error");
              
            }

            if (genres == null)
            {
                return Problem(statusCode: 400, detail: "Could not find object", title: "400 Error");
              
            }

            _unitOfWork.GenreRepository.Delete(genreId);
            _unitOfWork.Save();
            return Accepted();
        }

    }
}
