using Microsoft.AspNetCore.Mvc;

namespace TAN.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load());
        }
    }
}
