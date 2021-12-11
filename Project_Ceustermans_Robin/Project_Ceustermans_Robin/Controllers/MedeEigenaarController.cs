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
    public class MedeEigenaarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedeEigenaarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MedeEigenaarOverzicht()
        {
            //List<MedeEigenaar> medeEigenaren = new List<MedeEigenaar>();
            MedeEigenaarOverzichtViewModel vm = new MedeEigenaarOverzichtViewModel();
            vm.MedeEigenaren = await _context.MedeEigenaaren.ToListAsync();
            return View(vm);
        }

        public IActionResult CreateMedeEigenaar()
        {
            return View();
        }

        //CRUD

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMedeEigenaar([Bind("Voornaam,Familienaam")] MedeEigenaar medeEigenaar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medeEigenaar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MedeEigenaarOverzicht));
            }
            CreateMedeEigenaarViewModel vm = new CreateMedeEigenaarViewModel()
            {
                Voornaam = medeEigenaar.Voornaam,
                Familienaam = medeEigenaar.Familienaam
            };
            return View(vm);
        }

        public async Task<IActionResult> DetailMedeEigenaar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedeEigenaar medeEigenaar = await _context.MedeEigenaaren.FindAsync(id);
            if (medeEigenaar == null)
            {
                return NotFound();
            }
            MedeEigenaarDetailsViewModel vm = new MedeEigenaarDetailsViewModel()
            {
               Voornaam = medeEigenaar.Voornaam,
               Familienaam = medeEigenaar.Familienaam
            };
            return View(vm);
        }

        public async Task<IActionResult> DeleteMedeEigenaar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MedeEigenaar medeEigenaar = await _context.MedeEigenaaren.FirstOrDefaultAsync(y => y.MedeEigenaarID == id);
            if (medeEigenaar == null)
            {
                return NotFound();
            }
            else
            {
                _context.MedeEigenaaren.Remove(medeEigenaar);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MedeEigenaarOverzicht));
        }

        public async Task<IActionResult> EditMedeEigenaar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedeEigenaar medeEigenaar = await _context.MedeEigenaaren.FindAsync(id);

            if (medeEigenaar == null)
            {
                return NotFound();
            }

            EditMedeEigenaarViewModel vm = new EditMedeEigenaarViewModel()
            {
                Voornaam = medeEigenaar.Voornaam,
                Familienaam = medeEigenaar.Familienaam
            };
            return View(vm);
        }

        private bool MedeEigenaarExists(int id)
        {
            MedeEigenaar medeEigenaar = _context.MedeEigenaaren.Find(id);
            return medeEigenaar != null ? true : false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedeEigenaar(int id, [Bind("Voornaam,Familienaam")] MedeEigenaar medeEigenaar)
        {
            medeEigenaar.MedeEigenaarID = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medeEigenaar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedeEigenaarExists(medeEigenaar.MedeEigenaarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MedeEigenaarOverzicht));
            }
            EditMedeEigenaarViewModel vm = new EditMedeEigenaarViewModel()
            {
                Voornaam = medeEigenaar.Voornaam,
                Familienaam = medeEigenaar.Familienaam
            };
            return View(vm);
        }

    }
}
