using Shop.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopLocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShopLocations
        public ActionResult Index()
        {
            return View("~/Views/Admin/ShopLocation/Index.cshtml", db.Locations.ToList());
        }

        // GET: ShopLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopLocation shopLocation = db.Locations.Find(id);
            if (shopLocation == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ShopLocation/Details.cshtml", shopLocation);
        }

        // GET: ShopLocations/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/ShopLocation/Create.cshtml");
        }

        // POST: ShopLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShopLocationID,OpeningHour,ClosingHour,Phone,EMail,City,FullAddress,PostalCode")] ShopLocation shopLocation)
        {
            if (ModelState.IsValid)
            {
                shopLocation.OpeningHour = new DateTime(1990, 1, 1, 8, 0, 0);
                shopLocation.ClosingHour = new DateTime(1990, 1, 1, 18, 0, 0);
                db.Locations.Add(shopLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/ShopLocation/Index.cshtml", shopLocation);
        }

        // GET: ShopLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopLocation shopLocation = db.Locations.Find(id);
            if (shopLocation == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ShopLocation/Edit.cshtml", shopLocation);
        }

        // POST: ShopLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShopLocationID,OpeningHour,ClosingHour,Phone,EMail,City,FullAddress,PostalCode")] ShopLocation shopLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/ShopLocation/Index.cshtml", shopLocation);
        }

        // GET: ShopLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopLocation shopLocation = db.Locations.Find(id);
            if (shopLocation == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ShopLocation/Delete.cshtml", shopLocation);
        }

        // POST: ShopLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopLocation shopLocation = db.Locations.Find(id);
            db.Locations.Remove(shopLocation);
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
