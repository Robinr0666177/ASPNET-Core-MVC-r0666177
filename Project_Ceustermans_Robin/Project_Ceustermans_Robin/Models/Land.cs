using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Models
{
    public class Land
    {
        [Key]
        public int LandID { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        //navigatieproperties

        ICollection<Merk> Merken { get; set; }
    }
}
