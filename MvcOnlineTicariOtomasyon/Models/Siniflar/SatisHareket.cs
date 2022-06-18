using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        [Display(Name = "Satış ID")]
        public int Satisid { get; set; }
        //ürün
        //cari
        //personel
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }

        [Display(Name = "Toplam Tutar")]
        public decimal ToplamTutar { get; set; }

        //Bir satış hareketin 1 ürünü/carisi/personeli olabilir.

        [Display(Name = "Ürün ID")]
        public int Urunid { get; set; }

        [Display(Name = "Cari ID")]
        public int Cariid { get; set; }

        [Display(Name = "Personel ID")]
        public int Personelid { get; set; }
        public bool Durum { get; set; }

        [Display(Name = "Ürün")]
        public virtual Urun Urun { get; set; }
        public virtual Cariler Cariler { get; set; }
        public virtual Personel Personel { get; set; }
    }
}