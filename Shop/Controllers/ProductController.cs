﻿using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CategoryWithSubcategories allCats = new CategoryWithSubcategories();
        // GET: Product
        public ActionResult Index()
        {
            var viewModel = new ProductCategoryViewModel();

            viewModel.Product = db.Products.ToList();
            viewModel.Category = db.Categories.ToList();
            viewModel.MenuCategories = allCats.getAllCategories(db);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Index(string category)
        {
            var viewModel = new ProductCategoryViewModel();

            viewModel.Product = db.Products.ToList();
            viewModel.Category = db.Categories.ToList();
            viewModel.MenuCategories = allCats.getAllCategories(db);
            viewModel.CurrentCategory = category;
            return View(viewModel);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var viewModel = db.Products.Find(id);
            int i = 0;
            foreach (var item in db.Copies)
            {
                if (item.ProductID == viewModel.ProductID)
                {
                    i++;
                }
            }
            viewModel.MenuCategories = allCats.getAllCategories(db);

            return View(viewModel);
        }

        // GET: Product
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Manufacturer);
            return View("~/Views/Admin/Product/Index.cshtml", products.ToList());
        }

        // GET: Product/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Product/Details.cshtml", product);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name");
            return View("~/Views/Admin/Product/Create.cshtml");
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,CategoryID,Description,ShortDescription,PriceNetto,VatPercent,ActualPrice,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", product.ManufacturerID);
            return View("~/Views/Admin/Product/Create.cshtml", product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", product.ManufacturerID);
            return View("~/Views/Admin/Product/Edit.cshtml", product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,CategoryID,Description,ShortDescription,PriceNetto,VatPercent,ActualPrice,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", product.ManufacturerID);
            return View("~/Views/Admin/Product/Edit.cshtml", product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Product/Delete.cshtml", product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult AddToCart(Product product)
        {
            if (Session["cart"] == null)
            {
                var cart = new Dictionary<Product, int>();
                cart.Add(product, 1);
                Session["cart"] = cart;
            }
            else
            {
                var cart = Session["cart"] as Dictionary<Product, int>;
                var _productInCart = cart.Keys.FirstOrDefault(prod => product.ProductID == prod.ProductID);
                if (_productInCart != null)
                {
                    cart[_productInCart]++;
                }
                else
                {
                    cart.Add(product, 1);
                }

            }
            return RedirectToAction("Details", new { id = product.ProductID });
        }
        public ActionResult AddAnotherCopy(int productId)
        {
            var cart = Session["cart"] as Dictionary<Product, int>;
            var _productInCart = cart.Keys.FirstOrDefault(prod => productId == prod.ProductID);
            if (_productInCart != null)
            {
                cart[_productInCart]++;
            }
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            var viewModel = new CartViewModel(Session["cart"] as Dictionary<Product, int>);
            viewModel.MenuCategories = allCats.getAllCategories(db);
            return View(viewModel);
        }

        public ActionResult AddOrder()
        {
            var o = new Order();
            o.Date = DateTime.Now;
            o.WasPaid = false;
            o.UserID = "1";

            return RedirectToAction("Create", "Orders", new { products = Session["cart"] as Dictionary<Product, int> });
        }
        public ActionResult RemoveFromCart(int productId)
        {
            var cart = Session["cart"] as Dictionary<Product, int>;

            Product p = cart.Keys.First(prod => prod.ProductID == productId);

            cart[p]--;

            if (cart[p] == 0)
            {
                cart.Remove(p);
            }


            return RedirectToAction("Cart");
        }
        public ActionResult ClearCart()
        {
            Session["cart"] = null;

            return RedirectToAction("Cart");
        }
    }
}
