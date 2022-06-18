using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Mesajlar
    {
        [Key] //Primary Key
        [Display(Name = "Mesaj ID")]
        public int MesajID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Display(Name = "Gönderici")]
        public string Gonderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Display(Name = "Alıcı")]
        public string Alici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        [Display(Name = "İçerik")]
        public string icerik { get; set; }

        [Column(TypeName = "Smalldatetime")]
        public DateTime Tarih { get; set; }
    }
}