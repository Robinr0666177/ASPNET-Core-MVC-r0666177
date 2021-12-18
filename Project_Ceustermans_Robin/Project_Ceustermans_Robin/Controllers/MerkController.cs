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
        //voor de homepagina
        public async Task<IActionResult> MerkOverzicht()
        {
            MerkOverzichtViewModel viewModel = new MerkOverzichtViewModel();
            viewModel.Merken = await _context.Merken.Include(x => x.Land).ToListAsync();
            return View(viewModel);
        }

        //combobox opvullen met landen bij het aanmaken/bewerken merken
        public async Task<IActionResult> CreateMerkAsync()
        {
            CreateMerkViewModel viewModel = new CreateMerkViewModel();
            await LandenOpvullenCreateAsync(viewModel);
            return View(viewModel);
        }

        //helpermethode voor landen op te halen in de combobox

        public async Task LandenOpvullenCreateAsync(CreateMerkViewModel viewModel)
        {
            viewModel.Landen = new List<SelectListItem>();
            var landen = await _context.Landen.ToListAsync();
            foreach (var item in landen)
            {
                viewModel.Landen.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.LandID.ToString() });
            }
        }

        public async Task LandenOpvullenEditAsync(EditMerkViewModel viewModel)
        {
            viewModel.Landen = new List<SelectListItem>();
            var landen = await _context.Landen.ToListAsync();
            foreach (var item in landen)
            {
                viewModel.Landen.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.LandID.ToString() });
            }
        }

        //CRUD Merken
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
            await LandenOpvullenCreateAsync(viewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> DetailMerk(int? id)
        {
            if (id == null)
            {
                return NotFound();              
            }

            Merk merk = await _context.Merken.Include(x => x.Land).FirstOrDefaultAsync(y => y.MerkID == id);
            if (merk == null)
            {
                return NotFound();
            }
            MerkDetailsViewModel vm = new MerkDetailsViewModel()
            {
                Naam = merk.Naam,
                Land = merk.Land.Beschrijving,
                Beschrijving = merk.Beschrijving
            };
            return View(vm);
        }

        public async Task<IActionResult> DeleteMerk(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Merk merk = await _context.Merken.Include(x => x.Land).FirstOrDefaultAsync(y => y.MerkID == id);
            if (merk == null)
            {
                return NotFound();
            }
            else
            {
                _context.Merken.Remove(merk);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MerkOverzicht));
        }

        public async Task<IActionResult> EditMerk(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Merk merk = await _context.Merken.FindAsync(id);

            if (merk == null)
            {
                return NotFound();
            }

            EditMerkViewModel vm = new EditMerkViewModel()
            {
                Naam = merk.Naam,
                LandID = merk.LandID,
                Beschrijving = merk.Beschrijving
            };
            await LandenOpvullenEditAsync(vm);
            return View(vm);
        }

        private bool MerkExists(int id)
        {
            Merk klant = _context.Merken.Find(id);
            return klant != null ? true : false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMerk(int id, [Bind("Naam,LandID,Beschrijving")] Merk merk)
        {
            merk.MerkID = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerkExists(merk.MerkID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MerkOverzicht));              
            }
            EditMerkViewModel vm = new EditMerkViewModel()
            {
                Naam = merk.Naam,
                LandID = merk.LandID,
                Beschrijving = merk.Beschrijving
            };
            await LandenOpvullenEditAsync(vm);
            return View(vm);
        }

    }
}
