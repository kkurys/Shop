using Microsoft.AspNet.Identity;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderViewModel details = new OrderViewModel();
            details.Order = db.Orders.Find(id);
            if (details.Order == null)
            {
                return HttpNotFound();
            }

            var cart = new Dictionary<Product, int>();
            foreach (var copy in details.Order.Copies)
            {
                var _productInCart = cart.Keys.FirstOrDefault(prod => copy.ProductID == prod.ProductID);
                if (_productInCart != null)
                {
                    cart[_productInCart]++;
                }
                else
                {
                    cart.Add(db.Products.Find(copy.ProductID), 1);
                }
            }
            details.SetProducts(cart);

            return View(details);
        }



        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create([Bind(Include = "OrderID,Date,WasPaid,UserID")] Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            
            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserID);
            return RedirectToAction("Details", "Orders", new { id = order.OrderID });
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Date,WasPaid,UserID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public ActionResult Pay(int? id)
        {
            db.Orders.Find(id).WasPaid = true;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        public ActionResult Invoice(int id)
        {
            Invoice invoice = new Invoice();
            var empl = db.Employees.ToList();
            Random rnd = new Random();
            invoice.EmployeeID = empl[rnd.Next(0, empl.Count - 1)].EmployeeID;
            invoice.Date = DateTime.Now.Date;
            invoice.OrderID = id;
            ViewBag.DeliveryDataID = new SelectList(db.DeliveryDatas.ToList().FindAll(delivery => User.Identity.GetUserId() == delivery.UserID), "DeliveryDataID", "FullAddress");


            return View(invoice);
        }

        [HttpPost]
        public ActionResult Invoice(Invoice invoice)
        {
            invoice.Date = DateTime.Now.Date;
            var empl = db.Employees.ToList();
            Random rnd = new Random();
            invoice.EmployeeID = empl[rnd.Next(0, empl.Count - 1)].EmployeeID;
            db.Invoices.Add(invoice);
            db.SaveChanges();

            return RedirectToAction("Pay", new { id = invoice.OrderID });
        }

        [HttpGet]
        public ActionResult InvoiceDetails(int id)
        {
            var invoice = db.Invoices.ToList().Find(inv => id == inv.OrderID);

            return View(invoice);
        }
    }
}
