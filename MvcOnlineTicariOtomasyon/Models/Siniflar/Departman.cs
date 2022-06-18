using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Departman
    {
        [Key]
        public int Departmanid { get; set; }

        [Display(Name = "Departman Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        public string DepartmanAd { get; set; }
        public bool Durum { get; set; }

        //Bir departmanın birden fazla personeli olabilir.
        public ICollection<Personel> Personels{ get; set; }
    }
}