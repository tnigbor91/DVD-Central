using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace TAN.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
        public IActionResult Index()
        {
            var item = FormatManager.Load();
            ViewBag.Title = "Formats";
            return View(item);
        }

        public IActionResult Details(int id)
        {
            var item = FormatManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create a Format";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Format format)
        {
            try
            {
                int result = FormatManager.Insert(format);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            var item = FormatManager.LoadById(id);
            ViewBag.Title = "Edit Format: " + item.Description;
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Update(format, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(format);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = FormatManager.LoadById(id);
            ViewBag.Title = "Delete Format: " + item.Description;
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(format);
            }
        }
    }
}
