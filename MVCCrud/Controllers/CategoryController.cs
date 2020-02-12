using MVCCrud.DesignPatterns.SingletonPattern;
using MVCCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class CategoryController : Controller
    {
        //MVC'de siz yarattıgınız bir action'in üzerine bir yöntem attribute'u yazmazsanız o action otomatik olarak HTTPGet yontemiyle calısır...


            //Siz bir işlem yapacak iseniz MVC'de kesinlikle bir Action'a ihtiyacınız vardır. Cünkü MVC metot güdümlü calısır. MVC'de bir ACtion'in illa View'i olması zorunlu degildir (bir Action illa sayfa gostermek zorunda degildir.)

        NorthwindEntities db;
        public CategoryController()
        {
            db = DBTool.DBInstance;
        }


        // GET: Category
        public ActionResult CategoryList()
        {
          
            return View(db.Categories.ToList());
        }



        public ActionResult AddCategory()
        {
            //Siz bir Modeli sadece kullanıcıya bilgi göstermek icin gönderirsiniz... Eger siz sayfaya Model göndermiyorsanız ama buna ragmen sayfa sanki bir model gönderilmiş gibi bir model karsılıyorsa bu, View'dan Post olan action'a bir model gelecek demektir ... Yani artık kullanıcının yaptıgı işlem sayesinde benim Post olan action'ıma bir model geliyordur. Eger View'dan Post olan action'a bir model geliyorsa Action'in parametre ismi önemsizdir.
            return View();
        }

        //Bir Action'a  Post yöntemiyle ulasılabilecegini  belirtmek icin  sizin o action'in basına kesinlikle HttpPost attribute'u koymanız gerekir. Yoksa Action Get olarak algılanır... Sayfanın server'a bir veri gönderebilmesi icin en saglıklı yöntem (form'dan da yapılabilecek tek yöntem) Post yöntemidir...

            //Unutmayın ki Post işlemine veri gelebilmesi icin tasarımda kesinlikle Form olması gerekir.
        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
           
            db.Categories.Add(item);
            db.SaveChanges();
            return RedirectToAction("CategoryList"); //bu ifade RedirectToAction metodu sayesinde bizi istedigimiz bir Action'a yönlendirir...
        }

        //Silme İşlemleri

        public ActionResult DeleteCategory(int id)
        {
            db.Categories.Remove(db.Categories.Find(id));
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
       

        //Güncelleme İşlemleri

        public ActionResult UpdateCategory(int id)
        {
            return View(db.Categories.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category item)
        {
            Category guncellenecek = db.Categories.Find(item.CategoryID);
            db.Entry(guncellenecek).CurrentValues.SetValues(item);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}