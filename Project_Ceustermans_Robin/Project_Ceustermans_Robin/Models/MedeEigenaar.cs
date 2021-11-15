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

        [Required]
        public string Voornaam { get; set; }
        
        [Required]
        public string Familienaam { get; set; }

    }
}
