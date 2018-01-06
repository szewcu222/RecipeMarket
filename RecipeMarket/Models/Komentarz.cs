using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeMarket.Models
{

    [Table("Komentarze")]
    public partial class Komentarz
    {
        [Key]
        public int KomentarzID { get; set; }


        [Required]
        public string TrescKomentarza { get; set; }

        public DateTime DataDodania { get; set; }

        [Display(Name = "Czas Przygotowania")]
        public bool CzasPrzygotowaniaOK { get; set; }

        [Display(Name = "Stopien Trudnosci")]
        public bool StopienTrudnosciOK { get; set; }

        [Display(Name = "Ocena")]
        public int Ocena { get; set; }

        public int PrzepisID { get; set; }

        public string UzytkownikID { get; set; }

        public virtual Przepis Przepis { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; }
    }
}