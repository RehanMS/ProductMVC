using PagedList;
using ProductMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProductMVC.Controllers
{
    public class HomeController : Controller
    {
        ProductDBContext dbContext;
        public HomeController()
        {
            dbContext = new ProductDBContext();
        }
        public ActionResult Index()
        {
            var data = (from pro in dbContext.Product.ToList()
                        from cat in dbContext.Category.Where(x => x.CategoryId == pro.Categoryid).ToList()
                        select new MultiData()
                        {
                            products = pro,
                            categories = cat
                        }).ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            CreateViewModel obj = new CreateViewModel();
            obj.Category = dbContext.Category.ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            Product product = new Product();

            if (model != null && model.ProductName != null)
            {
                product.ProductName = model.ProductName;
                product.Categoryid = model.CategoryId;
                var isInserted = dbContext.Product.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Plz enter input";
                return View();
            }

        }
        
        public ActionResult Edit(int id)
        {
            var product = dbContext.Product.Where(x => x.ProductId == id).FirstOrDefault();
            CreateViewModel obj = new CreateViewModel();
            obj.ProductId = product.ProductId;
            obj.ProductName = product.ProductName;
            obj.CategoryId = product.Categoryid;
            obj.Category = dbContext.Category.ToList();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(CreateViewModel product)
        {
            Product obj = dbContext.Product.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            if(obj != null)
            {
                obj.ProductId = product.ProductId;
                obj.ProductName = product.ProductName;
                obj.Categoryid = product.CategoryId;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public ActionResult Details(int id)
        {
            var product = dbContext.Product.Where(x => x.ProductId == id).FirstOrDefault();
            CreateViewModel model = new CreateViewModel();
            model.ProductId = product.ProductId;
            model.ProductName = product.ProductName;
            model.CategoryId = product.Categoryid;
            return View(model);
        }
        public ActionResult Delete( int id)
        {
            var product = dbContext.Product.Where(x => x.ProductId == id).FirstOrDefault();
            CreateViewModel model = new CreateViewModel();
            model.ProductId = product.ProductId;
            model.ProductName = product.ProductName;
            model.CategoryId = product.Categoryid;
            return View(model);

        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Product model = dbContext.Product.Where(x => x.ProductId==id).FirstOrDefault();
            if (model != null)
            {
                dbContext.Product.Remove(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
        }

    }
}