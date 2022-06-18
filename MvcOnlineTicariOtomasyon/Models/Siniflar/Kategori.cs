using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Kategori
    {
        [Key] //Primary Key
        [Display(Name = "Kategori ID")]
        public int KategoriID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        [Display(Name = "Kategori Ad")]
        public string KategoriAd { get; set; }

        public bool Durum { get; set; }
        //Her kategoride 1'den fazla ürün vardır.
        public ICollection<Urun> Uruns { get; set; }
    }
}