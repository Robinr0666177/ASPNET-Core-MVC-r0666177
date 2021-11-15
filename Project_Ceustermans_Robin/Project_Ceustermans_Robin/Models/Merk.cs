using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Models
{
    public class Merk
    {
        [Key]
        public int MerkID { get; set; }

        [Required(ErrorMessage = "De naam van het merk moet opgevuld zijn!")]
        public string Naam { get; set; }

        [Required]
        public int LandID { get; set; }

        public string Beschrijving { get; set; }

    }
}
