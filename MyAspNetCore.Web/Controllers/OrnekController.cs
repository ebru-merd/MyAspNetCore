using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCore.Web.Controllers

{
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class OrnekController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.name = "Asp .Net Core";

            ViewData["age"] = 30;

            ViewData["names"] =new List<string>(){ "ebru","sude"};

            ViewBag.person = new {Id=1, Name = "ebru", Age = 20 };

            TempData["surname"] = "merd";


            var productList = new List<Product2>()
            {
                new(){Id=1, Name="ebru"},
                new(){Id=2, Name="sude"},
                new(){Id=3, Name="ece"}
            };
            return View(productList);
        }

        public IActionResult Index2()
        {
            var surName = TempData["surname"];
            return View();
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Ornek");
            //return View();
        }

        public IActionResult ContentResult()
        {
            return Content("ebru");
        }

        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonResultParametre", "Ornek", new { id = id });

        }

        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id });
        }

        public IActionResult JsonResult()
        {
            return Json(new {Id=1 , name="ebru", price=100});
        }

        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}
