using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }
    }
}
