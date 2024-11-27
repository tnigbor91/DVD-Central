
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using TAN.DVDCentral.UI.Models;

namespace TAN.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public IActionResult Index()
        {
            var item = OrderManager.Load();
            ViewBag.Title = "List of Orders";
            return View(item);
        }

        public IActionResult Details(int id)
        {
            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Details for Customer No. " + item.CustomerId;
            return View(item);
        }

        public IActionResult Create()
        {
            if(Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Create an Order";
                return View();
            }
                
            else
                return RedirectToAction("Login", "User", new {returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request)});
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                var item = OrderManager.LoadById(id);
                ViewBag.Title = "Edit Order";
                return View(item);
            }
                
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        [HttpPost]
        public IActionResult Edit(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Update(order, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Delete Order";
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }
    }
}
