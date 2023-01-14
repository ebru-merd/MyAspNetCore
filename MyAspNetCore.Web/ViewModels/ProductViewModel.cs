using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCore.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }


        [Remote(action: "HasProductName" , controller: "Products")]
        [StringLength(50, ErrorMessage ="isim alanına en fazla 50 karakter girilebilir.")]
        [Required(ErrorMessage ="İsim alanı boş olamaz.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş olamaz")]
        [Range(1, 1000, ErrorMessage = "Fiyat alanı 1 ile 1000 arasında bir değer olmalıdır.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stok alanı boş olamaz")]
        [Range(1,200 , ErrorMessage = "Stok alanı 1 ile 200 arasında bir değer olmalıdır.")]
        public int? Stock { get; set; }

        [StringLength(300,MinimumLength =50, ErrorMessage = "açıklama alanı 50 ile 300 karakter arasında olabilir..")]
        [Required(ErrorMessage = "Açıklama alanı boş olamaz.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Barkod alanı boş olamaz.")]
        public string? Barcode { get; set; }
        public bool IsPublish { get; set; }


        [Required(ErrorMessage = "Yayınlanma tarihi boş olamaz.")]
        public DateTime? PublishDate { get; set; }


        [Required(ErrorMessage = "Yayınlama Süresi boş olamaz.")]
        public int? Expire { get; set; }
    }
}
