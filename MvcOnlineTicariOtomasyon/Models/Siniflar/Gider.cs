 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Gider
    {
        [Key]
        [Display(Name = "Gider ID")]
        public int Giderid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter yazabilirsiniz.")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
    }
}