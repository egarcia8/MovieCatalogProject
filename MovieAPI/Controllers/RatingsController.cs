using Microsoft.AspNetCore.Mvc;
using MovieProject.Data;
using MovieProject.Models;

namespace MovieProject.Controllers
{
    [ApiController] //annotation to flag this as an apicontroller for the program to see
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RatingsController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        
        public RatingsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get a list of rating items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Ratings> Ratings()
        {
            //return _db.Ratings;
            //var ratings = from r in ratingsRepository.GetRatings()
            //              select r;
            var ratings = _unitOfWork.RatingRepository.Get();
            return ratings;

        }

        /// <summary>
        /// Get a rating by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  /// <response code="200">Returns the list of items</response>
        [HttpGet("{id}")]
        public ActionResult GetRating(int id)
        {
            Ratings rating = _unitOfWork.RatingRepository.GetByID(id);

            if (rating == null)
            {
                return NotFound("That object is not found.");
            }

            return Accepted(rating);

        }

        /// <summary>
        /// Create a new rating item
        /// </summary>
        /// <param name="postRating"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Ratings(RatingDto postRating)
        {
            var tempRating = new Ratings()
            {
                RatingId = postRating.RatingId,
                Rating = postRating.Rating
            };
            
            _unitOfWork.RatingRepository.Insert(tempRating);
            _unitOfWork.Save();
            return Accepted(tempRating);
        }

        /// <summary>
        /// Update a rating item
        /// </summary>
        /// <param name="ratingId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Ratings(int ratingId, RatingDto rating)
        {
            Ratings ratings = _unitOfWork.RatingRepository.GetByID(ratingId);
            if (ratings == null)
            {
                return Problem(statusCode: 400, detail: "Could not find object", title: "400 Error");
               
            }

            ratings.Rating = rating.Rating;

            _unitOfWork.RatingRepository.Update(ratings);
            _unitOfWork.Save();
            return Accepted();

        }

        /// <summary>
        /// Delete a rating item by id
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Ratings(int ratingId)
        {

            Ratings ratings = _unitOfWork.RatingRepository.GetByID(ratingId);

            var movie = _unitOfWork.MovieRepository.Get(m => m.RatingId == ratingId);
           
            if (movie.ToList().Count > 0)
            {
                return Problem(statusCode: 400, detail: "Cannot delete rating; other table has dependency on it", title: "400 Error");
                
            }
            if (ratings == null)
            {
                return Problem(statusCode: 400, detail: "Could not find object", title: "400 Error");
               
            }

            _unitOfWork.RatingRepository.Delete(ratingId);
            _unitOfWork.Save();
            return Accepted();
        }

    }
}

