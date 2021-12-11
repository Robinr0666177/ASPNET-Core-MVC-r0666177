using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.ViewModels
{
    public class EditMerkViewModel
    {

        public List<SelectListItem> Landen { get; set; }

        public int MerkID { get; set; }

        public string Naam { get; set; }

        public int LandID { get; set; }

        public string Beschrijving { get; set; }

    }
}
