using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Urun
    {
        [Key]
        [Display(Name = "Ürün ID")]
        public int Urunid { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        [Display(Name = "Ürün Ad")]
        public string UrunAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Marka { get; set; }
        public short Stok { get; set; } //Sql'de short smallint demektir.

        [Display(Name = "Alış Fiyat")]
        public decimal AlisFiyat { get; set; }

        [Display(Name = "Satış Fiyat")]
        public decimal SatisFiyat { get; set; }
        public bool Durum { get; set; } //Ürün stoğu için bir kritik seviye belirleyeceğiz. Bool olmasının sebebi true/false şeklinde bir veritipi olacak olması.

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        [Display(Name = "Ürün Görsel")]
        public string UrunGorsel { get; set; }

        //Bir ürünün sadece 1 kategorisi olabilir.
        //Virtual yaparak kategori sınıfına da erişebileceğiz.
        [Display(Name = "Kategori ID")]
        public int Kategoriid { get; set; }
        public virtual Kategori Kategori  { get; set; }

        //Bir ürünün birden fazla satış hareketi olabilir.
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}