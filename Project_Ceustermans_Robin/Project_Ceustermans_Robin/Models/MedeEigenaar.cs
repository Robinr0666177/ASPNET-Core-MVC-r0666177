using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project_Ceustermans_Robin.Models
{
    public class MedeEigenaar
    {
        [Key]
        public int MedeEigenaarID { get; set; }

        [Required(ErrorMessage ="De voornaam is verplicht!")]
        public string Voornaam { get; set; }
        
        [Required(ErrorMessage = "De familienaam is verplicht!")]
        public string Familienaam { get; set; }

        //navigateproperties
        public ICollection<MedeEigenaarObject> medeEigenaarObjecten { get; set; }

    }
}
