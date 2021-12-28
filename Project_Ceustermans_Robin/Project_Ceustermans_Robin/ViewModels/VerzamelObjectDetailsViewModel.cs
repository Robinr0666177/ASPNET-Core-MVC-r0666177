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
    public class VerzamelObjectDetailsViewModel
    {
        [Display(Name = "Afbeelding")]
        public string Afbeelding { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal? AankoopPrijs { get; set; }
        public decimal? Waarde { get; set; }
        public int? CreatieJaar { get; set; }
        public string Merk { get; set; }
        public string Categorie { get; set; }
        public int? Breedte_Cm { get; set; }
        public int? Hoogte_Cm { get; set; }
        public int? Lengte_Cm { get; set; }
        public ICollection<MedeEigenaarObject> MedeEigenarenObject { get; set; }     
    }
}
