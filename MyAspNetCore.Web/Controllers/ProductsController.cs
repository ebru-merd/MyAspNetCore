using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using MyAspNetCore.Web.Helpers;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.ViewModels;

namespace MyAspNetCore.Web.Controllers
{


    public class ProductsController : Controller
    {

        private  AppDbContext _context;

        private readonly IMapper _mapper;
        
        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context ,IMapper mapper)
        {

            //DI Container
            //Dependency Injection Pattern 
            _productRepository = new ProductRepository();
        
            _context = context;
            _mapper = mapper;

           /* //Linq method
            if (!_context.Products.Any()) 
            {
                _context.Products.Add(new Product() { Name = "kalem 1", Price = 10, Stock = 10 });
                _context.Products.Add(new Product() { Name = "kalem 2", Price = 20, Stock = 20 });
                _context.Products.Add(new Product() { Name = "kalem 3", Price = 30, Stock = 30 });

                //db ye yansıtma işlemi yapıyoruz.
                _context.SaveChanges();
            }   
           */


        }
        public IActionResult Index()
        {

            //doğru bir yaklaşım değil constructor oluştur.
            //IHelper helper = new Helper();

            var products = _context.Products.ToList();


            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult Remove(int id)
        {
            var product=_context.Products.Find(id);

            //bu kısımda Ef core a haber verdik
            _context.Products.Remove(product);

            //veritabanı ile haberleştik.
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        //Get sayfayı göstermek için kullanıyoruz
        public IActionResult Add()
        {

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay" , 1 },
                {"3 ay" , 3},
                {"6 ay" , 6},
                {"12 ay" , 12 }
            };



            ViewBag.BarcodeSelect = new SelectList(new List<BarcodeSelectList>()
            {
                new(){ Data="mavi" , Value="Mavi"},
                new(){ Data="kırmızı" , Value="kırmızı"},
                new(){ Data="sarı" , Value="sarı"}

            } , "Value" , "Data");

            return View();
        }

        [HttpPost]
        //kullanıcı butona bastığında  datayı kaydeder.Bunlar view sayfaları genellikle olmaz. Getlerin görüntülenecek sayfaları olur.
        public IActionResult Add(ProductViewModel newProduct)
        {

            //Request Header-Body

            //1. yöntem

            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var barcode = HttpContext.Request.Form["Barcode"].ToString();

            //2. yöntem
            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Barcode= Barcode };


            //if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            //{
            //    ModelState.AddModelError(String.Empty, "Ürün ismi A harfi ile başlayamaz.");
            //}

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay" , 1 },
                {"3 ay" , 3},
                {"6 ay" , 6},
                {"12 ay" , 12 }
            };



            ViewBag.BarcodeSelect = new SelectList(new List<BarcodeSelectList>()
            {
                new(){ Data="mavi" , Value="Mavi"},
                new(){ Data="kırmızı" , Value="kırmızı"},
                new(){ Data="sarı" , Value="sarı"}

            }, "Value", "Data");

            if (ModelState.IsValid)
            {


                try
                {
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();

                    TempData["status"] = "Ürün başarıyla eklendi.";

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.TryAddModelError(String.Empty , "ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz");
                }
              
            }
            else
            {

                return View();
            }

            return View();
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);


            ViewBag.radioExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay" , 1 },
                {"3 ay" , 3},
                {"6 ay" , 6},
                {"12 ay" , 12 }
            };



            ViewBag.BarcodeSelect = new SelectList(new List<BarcodeSelectList>()
            {
                new(){ Data="mavi" , Value="Mavi"},
                new(){ Data="kırmızı" , Value="kırmızı"},
                new(){ Data="sarı" , Value="sarı"}

            }, "Value", "Data" ,product.Barcode);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        //updateProduct değişkenini biz class üzerinden alırız. Bunlar bizim projemiz de RequestBodylerde bekler. int,decimal,string gibi aldığımız productId değişkenleri ise Requestlerin query stringlerinde bekler.
        public IActionResult Update(ProductViewModel updateProduct)
        {
            
            if (!ModelState.IsValid)
            {

                ViewBag.radioExpireValue = updateProduct.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay" , 1 },
                {"3 ay" , 3},
                {"6 ay" , 6},
                {"12 ay" , 12 }
            };



                ViewBag.BarcodeSelect = new SelectList(new List<BarcodeSelectList>()
            {
                new(){ Data="mavi" , Value="Mavi"},
                new(){ Data="kırmızı" , Value="kırmızı"},
                new(){ Data="sarı" , Value="sarı"}

            }, "Value", "Data", updateProduct.Barcode);

                return View();
            }

            
            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET","POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct=_context.Products.Any(x => x.Name.ToLower()== Name.ToLower());

            if (anyProduct)
            {
                return Json("ürün ismi veritabanında bulunmaktadır.");
            }
            else
            {
                return Json(true);
            }

        }
    }
}
