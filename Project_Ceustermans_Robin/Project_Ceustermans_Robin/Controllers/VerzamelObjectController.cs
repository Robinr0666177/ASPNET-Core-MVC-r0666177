using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Ceustermans_Robin.Data;
using Project_Ceustermans_Robin.Models;
using Project_Ceustermans_Robin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Controllers
{
    public class VerzamelObjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VerzamelObjectController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult VerzamelObjectOverzicht()
        {
            //lijst verzamelobjecten
            return View();
        }

        //voor object aan te maken
        public async Task<IActionResult> CreateVerzamelObjectAsync()
        {
            CreateVerzamelObjectViewModel viewModel = new CreateVerzamelObjectViewModel();
            await CategorieënOpvullenCreateAsync(viewModel);
            await MerkenOpvullenCreateAsync(viewModel);
            await MedeEigenarenOpvullenCreateAsync(viewModel);
            return View(viewModel);
        }

        public async Task MedeEigenarenOpvullenCreateAsync(CreateVerzamelObjectViewModel viewModel)
        {
            viewModel.MedeEigenaren = new List<SelectListItem>();
            var medeEigenaren = await _context.MedeEigenaaren.ToListAsync();
            foreach (var item in medeEigenaren)
            {
                viewModel.MedeEigenaren.Add(new SelectListItem() { Text = item.ToString(), Value = item.MedeEigenaarID.ToString() });
            }
        }

        public async Task MerkenOpvullenCreateAsync(CreateVerzamelObjectViewModel viewModel)
        {
            viewModel.Merken = new List<SelectListItem>();
            var merken = await _context.Merken.ToListAsync();
            foreach (var item in merken)
            {
                viewModel.Merken.Add(new SelectListItem() { Text = item.Naam, Value = item.MerkID.ToString() });
            }
        }

        public async Task CategorieënOpvullenCreateAsync(CreateVerzamelObjectViewModel viewModel)
        {
            viewModel.Categorieën = new List<SelectListItem>();
            var categorieën = await _context.Categories.ToListAsync();
            foreach (var item in categorieën)
            {
                viewModel.Categorieën.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.CategorieID.ToString() });
            }
        }

        //voor object te bewerken

        //test
        public IActionResult EditVerzamelObject()
        {
            EditVerzamelObjectViewModel viewModel = new EditVerzamelObjectViewModel();
            return View(viewModel);
        }

        //lijst opvullen van mede-eigenaren die kunnen gekozen worden bij het aanmaken van item

        //lijst opvullen van bestaande mede-eigenaren bij het bewerken van een item


        ////CRUD

        private string UploadedFileNew(CreateVerzamelObjectViewModel ViewModel)
        {
            string uniqueFileName = null;

            if (ViewModel.Afbeelding != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ViewModel.Afbeelding.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ViewModel.Afbeelding.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVerzamelObject(CreateVerzamelObjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFileNew(viewModel);

                VerzamelObject verzamelObject = new VerzamelObject()
                {
                    Naam = viewModel.Naam,
                    Beschrijving = viewModel.Beschrijving,
                    AankoopPrijs = viewModel.AankoopPrijs,
                    Waarde = viewModel.Waarde,
                    CreatieJaar = viewModel.CreatieJaar,
                    MerkID = viewModel.MerkID,
                    CategorieID = viewModel.CategorieID,
                    Breedte_Cm = viewModel.Breedte_Cm,
                    Hoogte_Cm = viewModel.Hoogte_Cm,
                    Lengte_Cm = viewModel.Lengte_Cm,
                    Afbeelding = uniqueFileName
                };
          
                _context.Add(verzamelObject);
                await _context.SaveChangesAsync();
                //
                return RedirectToAction(nameof(VerzamelObjectOverzicht));
            }
            return View();
        }
    }
}
