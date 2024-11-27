using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using TAN.DVDCentral.UI.Models;
using TAN.DVDCentral.UI.ViewModels;
namespace TAN.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            var item = MovieManager.Load();
            ViewBag.Title = "List of Movies";
            return View(item);
        }

        public IActionResult Browse(int id)
        {
            var item = MovieManager.Load(id);
            ViewBag.Title = "List of Movies";
            return View(item);
        }

        public IActionResult Details(int id)
        {
            var item = MovieManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Title;
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create a Movie";

            MovieVM movieVM = new MovieVM();

            movieVM.Movie = new BL.Models.Movie();
            movieVM.DirectorList = DirectorManager.Load();
            movieVM.GenreList = GenreManager.Load();
            movieVM.RatingList = RatingManager.Load();
            movieVM.FormatList = FormatManager.Load();

            return View(movieVM);
        }

        [HttpPost]
        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                int result = MovieManager.Insert(movieVM.Movie);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {

            MovieVM movieVM = new MovieVM();

            movieVM.Movie = MovieManager.LoadById(id);
            movieVM.GenreList = GenreManager.Load();
            movieVM.DirectorList = DirectorManager.Load();
            movieVM.FormatList = FormatManager.Load();
            movieVM.RatingList = RatingManager.Load();

            ViewBag.Title = "Edit " + movieVM.Movie.Title;

            if (Authenticate.IsAuthenticated(HttpContext))
                return View(movieVM);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Update(movieVM.Movie, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = MovieManager.LoadById(id);
            ViewBag.Title = "Delete " + item.Title;
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, Movie movie, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movie);
            }
        }
    }
}
