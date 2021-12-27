using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.ViewModels
{
    public class AddDeleteVerzamelObjectViewModel
    {
        public List<SelectListItem> MedeEigenarenVerzamelObjecten { get; set; }

        public List<SelectListItem> VrijeMedeEigenaren { get; set; }

    }
}
