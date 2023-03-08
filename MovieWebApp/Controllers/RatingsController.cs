using Microsoft.AspNetCore.Mvc;


namespace MoviesList.Controllers
{
    public class RatingsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        // GET: Ratings/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(id);
        }


    }
}