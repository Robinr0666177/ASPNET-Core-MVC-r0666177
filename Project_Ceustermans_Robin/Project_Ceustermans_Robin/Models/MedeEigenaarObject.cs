using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project_Ceustermans_Robin.Models
{
    public class MedeEigenaarObject
    {
        [Required]
        public int MedeEigenaarID { get; set; }

        [Required]
        public int ObjectID { get; set; }

    }
}
