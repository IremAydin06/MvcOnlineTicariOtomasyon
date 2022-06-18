using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int Personelid { get; set; }

        [Display(Name = "Personel Ad")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        public string PersonelAd { get; set; }

        [Display(Name = "Personel Soyad")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        public string PersonelSoyad { get; set; }

        [Display(Name = "Personel Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(250, ErrorMessage = "En fazla 250 karakter yazabilirsiniz.")]
        public string PersonelGorsel { get; set; } //String türünde tutmamızın nedeni görselin kendisini değil bulut üzerindeki yolunu kullanacak olmamız. Kendisini kullanırsak hafıza çok şişer.

        public bool Durum { get; set; }
        //Bir personelin birden fazla satış hareketi olabilir.
        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public int Departmanid { get; set; }
        //Bir personelin 1 departmanı olabilir.
        public virtual Departman Departman { get; set; }
    }
}