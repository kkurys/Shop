using Microsoft.AspNet.Identity;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
        [Authorize(Roles = "Admin, Employee")]

        public ActionResult AdminIndex()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Manufacturer);
            return View("~/Views/Admin/Product/Index.cshtml", products.ToList());
        }

        // GET: Product/Details/5
        [Authorize(Roles = "Admin, Employee")]

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
        [Authorize(Roles = "Admin, Employee")]

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
        [Authorize(Roles = "Admin, Employee")]

        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,CategoryID,Description,ShortDescription,PriceNetto,VatPercent,ActualPrice,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                product.ActualPrice = product.PriceNetto + product.VatPercent * product.PriceNetto / 100;
                db.SaveChanges();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase img = Request.Files[i];
                    img.SaveAs(HttpContext.Server.MapPath("~/Content/Images/Products/")
                             + img.FileName);
                    ProductImage _productImage = new ProductImage();
                    _productImage.Product = product;
                    _productImage.FileName = "~/Content/Images/Products/" + img.FileName;
                    db.ProductImages.Add(_productImage);
                    db.SaveChanges();
                }
                return RedirectToAction("AdminIndex");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", product.ManufacturerID);
            return View("~/Views/Admin/Product/Create.cshtml", product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin, Employee")]

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
        [Authorize(Roles = "Admin, Employee")]

        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,CategoryID,Description,ShortDescription,PriceNetto,VatPercent,ActualPrice,ManufacturerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", product.ManufacturerID);
            return View("~/Views/Admin/Product/Edit.cshtml", product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin, Employee")]

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
        [Authorize(Roles = "Admin, Employee")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
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
                var availableCopies = db.Products.Find(productId).Copies.Count(cop => cop.WasSold == false);
                if (cart[_productInCart] < availableCopies)
                {
                    cart[_productInCart]++;
                }
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
            o.Copies = new List<Copy>();
            var cart = Session["cart"] as Dictionary<Product, int>;
            foreach (var product in cart.Keys)
            {
                var _productInCart = cart.Keys.FirstOrDefault(prod => product.ProductID == prod.ProductID);
                int max = cart[_productInCart];
                int current = 0;
                while (current != max)
                {
                    var tmpList = db.Copies.ToList();
                    for (int i = 0; i < tmpList.Count(); i++)
                    {
                        if (tmpList[i].WasSold == false)
                        {
                            o.Copies.Add(tmpList[i]);
                            db.Copies.Find(tmpList[i].CopyID).WasSold = true;
                            db.SaveChanges();
                            current++;
                            break;
                        }
                    }
                }
            }

            o.Date = DateTime.Now;
            o.WasPaid = false;
            o.UserID = User.Identity.GetUserId();


            db.Orders.Add(o);
            db.SaveChanges();
            Session["cart"] = null;
            return RedirectToAction("Details", "Orders", new { id = o.OrderID });
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
