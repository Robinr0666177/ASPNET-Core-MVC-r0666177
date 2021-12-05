
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

        public async Task<IActionResult> MerkOverzicht()
        {
            List<Merk> merken = new List<Merk>();
            MerkOverzichtViewModel vm = new MerkOverzichtViewModel();
            vm.Merken = await _context.Merken.Include(x => x.Land).ToListAsync();
            return View(vm);
        }

        //combobox opvullen met landen bij het aanmaken/bewerken merken
        public async Task<IActionResult> CreateMerkAsync()
        {
            CreateMerkViewModel viewModel = new CreateMerkViewModel();
            await LandenOpvullenAsync(viewModel);
            //viewModel.Landen = new List<SelectListItem>();
            //var landen = await _context.Landen.ToListAsync();
            //foreach (var item in landen)
            //{
            //    viewModel.Landen.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.LandID.ToString() });
            //}
            return View(viewModel);
        }

        //helpermethode voor landen op te halen

        public async Task LandenOpvullenAsync(CreateMerkViewModel viewModel)
        {
            viewModel.Landen = new List<SelectListItem>();
            var landen = await _context.Landen.ToListAsync();
            foreach (var item in landen)
            {
                viewModel.Landen.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.LandID.ToString() });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMerk([Bind("MerkID,Naam,LandID,Beschrijving")] Merk merk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MerkOverzicht));
            }

            CreateMerkViewModel viewModel = new CreateMerkViewModel()
            {
                Naam = merk.Naam,
                LandID = merk.LandID,
                Beschrijving = merk.Beschrijving
            };
            await LandenOpvullenAsync(viewModel);
            //CreateMerkAsync()
            return View(viewModel);
        }



      
    }
}
