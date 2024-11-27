using Microsoft.AspNetCore.Mvc;

namespace TAN.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            var item = OrderItemManager.Load();
            ViewBag.Title = "List of Order Items";
            return View(item);
        }
    }
}
