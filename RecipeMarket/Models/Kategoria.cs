using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeMarket.Models
{
    [Table("Kategorie")]
    public partial class Kategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategoria()
        {
            Przepisy = new HashSet<Przepis>();
        }

        [Key]
        public int KategoriaID { get; set; }

        [Required]
        public string NazwaKategorii { get; set; }

        [Required]
        public string OpisKategorii { get; set; }

        public StatusAkceptacji StatusAkceptacji { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Przepis> Przepisy { get; set; }
    }
}