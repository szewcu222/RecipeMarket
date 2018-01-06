using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeMarket.Models
{

    [Table("Przepisy")]
    public partial class Przepis
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Przepis()
        {
            Komentarze = new HashSet<Komentarz>();
            Skladniki = new HashSet<Skladnik>();
            Kategorie = new HashSet<Kategoria>();
        }

        [Key]
        public int PrzepisID { get; set; }

        [Display(Name = "Nazwa przepisu:")]
        [Required(ErrorMessage = "Brak nazwy przepisu!")]
        public string NazwaPrzepisu { get; set; }

        [Display(Name = "Tresc przepisu:")]
        [Required(ErrorMessage = "Brak tresci przepisu!")]
        [DataType(DataType.MultilineText)]
        public string TrescPrzepisu { get; set; }

        public decimal Ocena { get; set; }

        public int LiczbaOcen { get; set; }

        [Required]
        [Display(Name = "Czas przygotowania:")]
        [DataType(DataType.Time)]
        public TimeSpan CzasPrzygotowania { get; set; }

        [Required]
        [Display(Name = "Stopien trudnosci:")]
        [Range(0, 10, ErrorMessage = "Błędne wartości! Wpisz wartość od 0-10.")]
        public int StopienTrudnosci { get; set; }

        [Display(Name = "Przepis premium::")]
        public bool IsPremium { get; set; }

        [Display(Name = "Data dodania:")]
        public DateTime DataDodania { get; set; }

        public StatusAkceptacji StatusAkceptacji { get; set; }

        [Display(Name = "Numęr Śąłóć")]
        public string UzytkownikID { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Komentarz> Komentarze { get; set; }


        [Display(Name = "Składniki")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Skladnik> Skladniki { get; set; }

        public virtual Uzytkownik Uzytkownicy { get; set; }

        [Display(Name = "Kategoria")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategoria> Kategorie { get; set; }
    }
}