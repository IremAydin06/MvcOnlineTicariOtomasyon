using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Yapilacak
    {
        [Key]
        [Display(Name = "Yapılacak ID")]
        public int Yapilacakid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Column(TypeName = "bit")]
        public bool Durum { get; set; }

    }
}