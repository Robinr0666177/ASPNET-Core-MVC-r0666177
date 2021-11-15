using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project_Ceustermans_Robin.Models
{
    public class VerzamelObject
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "De naam van het verzamelstuk moet opgevuld zijn!")]
        public string Naam { get; set; }

        public string  Beschrijving { get; set; }
        public decimal? AankoopPrijs { get; set; }
        public decimal? Waarde { get; set; }
        public int? CreatieJaar { get; set; }
        public int? MerkID { get; set; }

        [Required(ErrorMessage = "Het verzamelobject dient een categorie te hebben, als deze geen heeft, selecteer 'overig'")]
        public int CategorieID { get; set; }
        public int? Breedte_Cm { get; set; }
        public int? Hoogte_Cm { get; set; }
        public int? Lengte_Cm { get; set; }
        public string Afbeelding { get; set; }

        //navigateproperties
        public ICollection<MedeEigenaarObject> medeEigenaarObjecten { get; set; }

        public Merk Merk { get; set; }

        public Categorie Categorie { get; set; }

    }
}
