using Microsoft.AspNetCore.Mvc;

namespace MoviesList.Controllers
{
    public class GenresController : Controller
    {

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View();
        }


    }
}
