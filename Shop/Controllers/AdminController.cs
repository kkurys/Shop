using Shop.Models;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult News()
        {
            return RedirectToAction("Index", "News");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Employee()
        {
            return RedirectToAction("Index", "Employee");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShopLocation()
        {
            return RedirectToAction("Index", "ShopLocation");
        }
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Product()
        {
            return RedirectToAction("AdminIndex", "Product");
        }
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Manufacturer()
        {
            return RedirectToAction("Index", "Manufacturer");
        }
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Category()
        {
            return RedirectToAction("Index", "Category");
        }
    }
}