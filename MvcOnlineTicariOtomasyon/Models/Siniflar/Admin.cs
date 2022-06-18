using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Admin
    {
        [Key] //Key olunca otomatik artan oluyor.
        public int Adminid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter yazabilirsiniz.")]
        public string KullaniciAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        public string Sifre { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1, ErrorMessage = "En fazla 1 karakter yazabilirsiniz.")]
        public string Yetki { get; set; }
    }
}