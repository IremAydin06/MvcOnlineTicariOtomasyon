using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Cariid { get; set; }

        [Display(Name = "Cari Ad")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        public string CariAd { get; set; }

        [Display(Name = "Cari Soyad")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
        public string CariSoyad { get; set; }

        [Display(Name = "Cari Şehir")]
        [Column(TypeName = "Varchar")]
        [StringLength(13, ErrorMessage = "En fazla 13 karakter yazabilirsiniz.")]
        public string CariSehir { get; set; }

        [Display(Name = "Cari Mail")]
        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter yazabilirsiniz.")]
        public string CariMail { get; set; }

        [Display(Name = "Cari Mail")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CariSifre { get; set; }
        public bool Durum { get; set; }

        //Bir carinin birden fazla satış hareketi olabilir.
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}