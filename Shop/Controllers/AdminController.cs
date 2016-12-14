using Shop.Models;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult News()
        {
            return RedirectToAction("Index", "News");
        }


        public ActionResult Employee()
        {
            return RedirectToAction("Index", "Employee");
        }
        public ActionResult ShopLocation()
        {
            return RedirectToAction("Index", "ShopLocation");
        }
        public ActionResult Product()
        {
            return RedirectToAction("AdminIndex", "Product");
        }
        public ActionResult Manufacturer()
        {
            return RedirectToAction("Index", "Manufacturer");
        }
        public ActionResult Category()
        {
            return RedirectToAction("Index", "Category");
        }
    }
}