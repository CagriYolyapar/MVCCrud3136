using MVCCrud.DesignPatterns.SingletonPattern;
using MVCCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities db;

        public ProductController()
        {
            db = DBTool.DBInstance;
        }
        // GET: Product
        public ActionResult ProductList()
        {
            return View(db.Products.ToList());
        }


        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product item)
        {
            db.Products.Add(item);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            db.Products.Remove(db.Products.Find(id));
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }


        public ActionResult UpdateProduct(int id)
        {
            return View(db.Products.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product item)
        {
            Product guncellenecek = db.Products.Find(item.ProductID);
            db.Entry(guncellenecek).CurrentValues.SetValues(item);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }


    }
}