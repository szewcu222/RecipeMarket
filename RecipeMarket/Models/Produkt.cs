using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeMarket.Models
{
    [Table("Produkty")]
    public partial class Produkt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produkt()
        {
            Skladniki = new HashSet<Skladnik>();
        }

        [Key]
        public int ProduktID { get; set; }

        [Required]
        public string NazwaProduktu { get; set; }

        public int Kalorie { get; set; }

        public int Bialko { get; set; }

        public int Weglowodany { get; set; }

        public int Tluszcz { get; set; }

        public StatusAkceptacji StatusAkceptacji { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Skladnik> Skladniki { get; set; }
    }
}