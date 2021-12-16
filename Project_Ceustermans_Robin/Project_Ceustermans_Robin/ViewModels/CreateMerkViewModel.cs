using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.ViewModels
{
    public class CreateMerkViewModel
    {
        [Required(ErrorMessage = "Er dient een geldig land gekozen te zijn!")]
        public List<SelectListItem> Landen { get; set; }

        public string  Naam { get; set; }

        [Required(ErrorMessage = "Er dient een geldig land gekozen te zijn!")]
        public int LandID { get; set; }

        public string Beschrijving { get; set; }

    }
}
