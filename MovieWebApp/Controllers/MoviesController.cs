using Microsoft.AspNetCore.Mvc;


namespace MoviesList.Controllers
{
    public class MoviesController : Controller
    {

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(id);
        }

    }
}

