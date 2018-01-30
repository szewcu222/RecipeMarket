using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeMarket.Models
{
    public class PrzepisyViewModel
    {
        public Przepis Przepis { get; set; }
        [Display(Name = "Kategoria")]
        public Kategoria Kategoria { get; set; }

        public IEnumerable<SelectListItem> ListaKategorii { get; set; }
    }

}