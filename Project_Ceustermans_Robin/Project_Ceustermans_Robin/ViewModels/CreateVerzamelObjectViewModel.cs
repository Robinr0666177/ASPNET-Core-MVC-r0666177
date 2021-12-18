using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.ViewModels
{
    public class CreateVerzamelObjectViewModel
    {
        public List<SelectListItem> MedeEigenaren { get; set; }

        public List<SelectListItem> Categorieën { get; set; }

        public List<SelectListItem> Merken { get; set; }
        //
        [Required(ErrorMessage = "Het object dient een naam te hebben!")]
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal? AankoopPrijs { get; set; }
        public decimal? Waarde { get; set; }
        public int? CreatieJaar { get; set; }
        public int? MerkID { get; set; }
        [Required(ErrorMessage = "Selecteer een categorie!")]
        public int? CategorieID { get; set; }
        public int? Breedte_Cm { get; set; }
        public int? Hoogte_Cm { get; set; }
        public int? Lengte_Cm { get; set; }
        [Display(Name = "Afbeelding")]
        public IFormFile Afbeelding { get; set; }
    }
}
