using Shop.Models;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CategoryWithSubcategories allCats = new CategoryWithSubcategories();

        public ActionResult Index()
        {
            var news = db.News.ToList();
            news.Reverse();
            return View(new HomeViewModel()
            {
                Categories = db.Categories.ToList(),
                News = news,


                MenuCategories = allCats.getAllCategories(db)

            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(new BaseViewModel()
            {
                Categories = db.Categories.ToList(),
                MenuCategories = allCats.getAllCategories(db)
            });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new BaseViewModel()
            {
                Categories = db.Categories.ToList(),
                MenuCategories = allCats.getAllCategories(db)
            });
        }
        public ActionResult Category(int id)
        {
            return RedirectToAction("Category", "Category", new { id = id });
        }
    }
}