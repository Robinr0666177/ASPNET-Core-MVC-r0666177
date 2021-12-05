
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Ceustermans_Robin.Data;
using Project_Ceustermans_Robin.Models;
using Project_Ceustermans_Robin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Controllers
{
    public class MerkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MerkController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        

        public IActionResult Merk()
        {
            return View();
        }


        public async Task<IActionResult> CreateMerkAsync()
        {
            CreateMerkViewModel viewModel = new CreateMerkViewModel();
            viewModel.Landen = new List<SelectListItem>();
            var landen = await _context.Landen.ToListAsync();
            foreach (var item in landen)
            {
                viewModel.Landen.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.LandID.ToString() });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Create([Bind("MerkID,Naam,LandID,Beschrijving")] Merk merk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateMerkViewModel viewModel = new CreateMerkViewModel()
            {
                Naam = merk.Naam,
                LandID = merk.LandID,
                Beschrijving = merk.Beschrijving
            };
            return View(viewModel);
        }



      
    }
}
