using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Controllers
{
    public class MerkController : Controller
    {
        public IActionResult Merk()
        {
            return View();
        }


        public IActionResult CreateMerk()
        {
            return View();
        }
    }
}
