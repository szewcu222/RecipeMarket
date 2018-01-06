using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeMarket.Models
{
    [Table("Skladniki")]
    public partial class Skladnik
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrzepisID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProduktID { get; set; }

        public decimal Ilosc { get; set; }


        public virtual Produkt Produkt { get; set; }

        public virtual Przepis Przepis { get; set; }
    }
}