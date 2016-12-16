using Shop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin, Employee")]

    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.BaseCategory);
            return View("~/Views/Admin/Category/Index.cshtml", categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Category/Details.cshtml", category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            var _categoriesToDisplay = new List<Category>(db.Categories);

            _categoriesToDisplay.Insert(0, new Category() { Name = "", CategoryID = -1 });

            ViewBag.BaseCategoryID = new SelectList(_categoriesToDisplay, "CategoryID", "Name");
            return View("~/Views/Admin/Category/Create.cshtml");
        }


        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name,BaseCategoryID")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.BaseCategoryID == -1)
                {
                    category.BaseCategoryID = null;
                }
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaseCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.BaseCategoryID);
            return View("~/Views/Admin/Category/Create.cshtml", category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.BaseCategoryID);
            return View("~/Views/Admin/Category/Edit.cshtml", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name,BaseCategoryID")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaseCategoryID = new SelectList(db.Categories, "CategoryID", "Name", category.BaseCategoryID);
            return View("~/Views/Admin/Category/Edit.cshtml", category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Category/Delete.cshtml", category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
