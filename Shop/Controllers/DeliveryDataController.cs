using Microsoft.AspNet.Identity;
using Shop.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class DeliveryDataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Manage");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryData deliveryData = db.DeliveryDatas.Find(id);
            if (deliveryData == null)
            {
                return HttpNotFound();
            }
            return View(deliveryData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryDataID,UserID,Name,Surname,EMail,Phone,City,FullAddress,PostalCode")] DeliveryData deliveryData)
        {
            if (ModelState.IsValid)
            {
                deliveryData.UserID = User.Identity.GetUserId();
                db.DeliveryDatas.Add(deliveryData);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }

            return View(deliveryData);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryData deliveryData = db.DeliveryDatas.Find(id);
            if (deliveryData == null)
            {
                return HttpNotFound();
            }

            return View(deliveryData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryDataID,UserID,Name,Surname,EMail,Phone,City,FullAddress,PostalCode")] DeliveryData deliveryData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            return View(deliveryData);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryData deliveryData = db.DeliveryDatas.Find(id);
            if (deliveryData == null)
            {
                return HttpNotFound();
            }
            return View(deliveryData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryData deliveryData = db.DeliveryDatas.Find(id);
            db.DeliveryDatas.Remove(deliveryData);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
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
