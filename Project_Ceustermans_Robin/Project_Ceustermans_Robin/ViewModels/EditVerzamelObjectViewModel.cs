using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Ceustermans_Robin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.ViewModels
{
    public class EditVerzamelObjectViewModel
    {
        public List<MedeEigenaarObject> MedeEigenaarObjecten { get; set; }
        public List<MedeEigenaar> MedeEigenaren { get; set; }
        public List<SelectListItem> Categorieën { get; set; }
        public List<SelectListItem> Merken { get; set; }
        //
        public int VerzamelObjectID { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal? AankoopPrijs { get; set; }
        public decimal? Waarde { get; set; }
        public int? CreatieJaar { get; set; }
        public int? MerkID { get; set; }
        public int? MedeEigenaarID { get; set; }
        public int CategorieID { get; set; }
        public int? Breedte_Cm { get; set; }
        public int? Hoogte_Cm { get; set; }
        public int? Lengte_Cm { get; set; }
        [Display(Name = "Afbeelding")]
        public string Afbeelding { get; set; }

        public IFormFile NieuweAfbeelding { get; set; }

    }
}
