using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> VerzamelObjectOverzicht()
        {
            VerzamelObjectOverzichtViewModel viewModel = new VerzamelObjectOverzichtViewModel();
            viewModel.Items = await _context.VerzamelObjecten.Include(x => x.Merk)
                                                             .Include(x => x.Categorie)
                                                             .ToListAsync();
            return View(viewModel);
        }



        //Details object


        
        public async Task<IActionResult> DetailVerzamelObject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VerzamelObject verzamelObject = await _context.VerzamelObjecten.Include(x => x.Merk).Include(x => x.Categorie)
                                                                           .Include(x => x.medeEigenaarObjecten).ThenInclude(y => y.MedeEigenaar)
                                                                           .FirstOrDefaultAsync(x => x.ID == id);
            if (verzamelObject != null)
            {
                string merk = "";
                if (verzamelObject.Merk != null)
                {
                    merk = verzamelObject.Merk.Naam;
                }
                else
                {
                    merk = "";
                }
                VerzamelObjectDetailsViewModel viewModel = new VerzamelObjectDetailsViewModel()
                {
                    Afbeelding = verzamelObject.Afbeelding,
                    Naam = verzamelObject.Naam,
                    Beschrijving = verzamelObject.Beschrijving,
                    AankoopPrijs = verzamelObject.AankoopPrijs,
                    Waarde = verzamelObject.Waarde,
                    CreatieJaar = verzamelObject.CreatieJaar,
                    Merk = merk,
                    Categorie = verzamelObject.Categorie.Beschrijving,
                    Breedte_Cm = verzamelObject.Breedte_Cm,
                    Hoogte_Cm = verzamelObject.Hoogte_Cm,
                    Lengte_Cm = verzamelObject.Lengte_Cm,
                    MedeEigenarenObject = verzamelObject.medeEigenaarObjecten
                };
                return View(viewModel);
            }
            return RedirectToAction(nameof(VerzamelObjectOverzicht));
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
                return RedirectToAction(nameof(VerzamelObjectOverzicht));
            }
            return await CreateVerzamelObjectAsync();
        }



        //voor object te bewerken



        public async Task MerkenOpvullenEditAsync(EditVerzamelObjectViewModel viewModel)
        {
            viewModel.Merken = new List<SelectListItem>();
            var merken = await _context.Merken.ToListAsync();
            foreach (var item in merken)
            {
                viewModel.Merken.Add(new SelectListItem() { Text = item.Naam, Value = item.MerkID.ToString() });
            }
        }

        public async Task CategorieënOpvullenEditAsync(EditVerzamelObjectViewModel viewModel)
        {
            viewModel.Categorieën = new List<SelectListItem>();
            var categorieën = await _context.Categories.ToListAsync();
            foreach (var item in categorieën)
            {
                viewModel.Categorieën.Add(new SelectListItem() { Text = item.Beschrijving, Value = item.CategorieID.ToString() });
            }
        }

        public async Task VerzamelObjectMedeEigenarenOpvullen(EditVerzamelObjectViewModel viewModel, int? id)
        {
            viewModel.MedeEigenaarObjecten = await _context.MedeEigenaarObjecten.Include(x => x.MedeEigenaar).Where(y => y.ObjectID == id).ToListAsync();
        }

        public async Task VrijeMedeEigenarenOpvullen(EditVerzamelObjectViewModel viewModel, int? id)
        {
            var medeEigenarenObjectIDs = await _context.MedeEigenaarObjecten.Include(x => x.MedeEigenaar).Where(y => y.ObjectID == id).Select(z => z.MedeEigenaarID).ToListAsync();
            viewModel.MedeEigenaren = await _context.MedeEigenaaren.Where(x => !medeEigenarenObjectIDs.Contains(x.MedeEigenaarID)).ToListAsync();         
        }

        public async Task<IActionResult> EditVerzamelObject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VerzamelObject verzamelObject = await _context.VerzamelObjecten.FindAsync(id);

            if (verzamelObject != null)
            {
                EditVerzamelObjectViewModel viewModel = new EditVerzamelObjectViewModel()
                {
                    VerzamelObjectID = verzamelObject.ID,
                    Afbeelding = verzamelObject.Afbeelding,
                    Naam = verzamelObject.Naam,
                    Beschrijving = verzamelObject.Beschrijving,
                    AankoopPrijs = verzamelObject.AankoopPrijs,
                    Waarde = verzamelObject.Waarde,
                    CreatieJaar = verzamelObject.CreatieJaar,
                    MerkID = verzamelObject.MerkID,
                    CategorieID = (int)verzamelObject.CategorieID,
                    Breedte_Cm = verzamelObject.Breedte_Cm,
                    Hoogte_Cm = verzamelObject.Hoogte_Cm,
                    Lengte_Cm = verzamelObject.Lengte_Cm

                };
                await MerkenOpvullenEditAsync(viewModel);
                await CategorieënOpvullenEditAsync(viewModel);
                await VerzamelObjectMedeEigenarenOpvullen(viewModel, id);
                await VrijeMedeEigenarenOpvullen(viewModel, id);
                return View(viewModel);
            }
            return RedirectToAction(nameof(VerzamelObjectOverzicht));

        }

        private bool VerzamelObjectExists(int id)
        {
            VerzamelObject verzamelObject = _context.VerzamelObjecten.Find(id);
            return verzamelObject != null ? true : false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVerzamelObject(int id, EditVerzamelObjectViewModel viewModel, VerzamelObject verzamelObject)
        {
            verzamelObject.ID = id;
            if (ModelState.IsValid)
            {
                try
                {
                    verzamelObject = _context.VerzamelObjecten.Find(id);
                    verzamelObject.Afbeelding = UploadedFileExisting(viewModel, oldImage: verzamelObject.Afbeelding);
                    verzamelObject.Naam = viewModel.Naam;
                    verzamelObject.Beschrijving = viewModel.Beschrijving;
                    verzamelObject.AankoopPrijs = viewModel.AankoopPrijs;
                    verzamelObject.Waarde = viewModel.Waarde;
                    verzamelObject.CreatieJaar = viewModel.CreatieJaar;
                    verzamelObject.MerkID = viewModel.MerkID;
                    verzamelObject.CategorieID = viewModel.CategorieID;
                    verzamelObject.Breedte_Cm = viewModel.Breedte_Cm;
                    verzamelObject.Hoogte_Cm = viewModel.Hoogte_Cm;
                    verzamelObject.Lengte_Cm = viewModel.Lengte_Cm;                   
                    _context.Update(verzamelObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerzamelObjectExists(verzamelObject.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(VerzamelObjectOverzicht));
            }

            EditVerzamelObjectViewModel vm = new EditVerzamelObjectViewModel()
            {
                VerzamelObjectID = verzamelObject.ID,
                //de afbeelding blijft bestaan maar na een mislukte update zal deze niet weergeven worden, ik weet niet waarom
                Afbeelding = verzamelObject.Afbeelding,
                Naam = verzamelObject.Naam,
                Beschrijving = verzamelObject.Beschrijving,
                AankoopPrijs = verzamelObject.AankoopPrijs,
                Waarde = verzamelObject.Waarde,
                CreatieJaar = verzamelObject.CreatieJaar,
                MerkID = verzamelObject.MerkID,
                CategorieID = (int)verzamelObject.CategorieID,
                Breedte_Cm = verzamelObject.Breedte_Cm,
                Hoogte_Cm = verzamelObject.Hoogte_Cm,
                Lengte_Cm = verzamelObject.Lengte_Cm              
            };

            await MerkenOpvullenEditAsync(viewModel);
            await CategorieënOpvullenEditAsync(viewModel);
            await VerzamelObjectMedeEigenarenOpvullen(viewModel, id);
            await VrijeMedeEigenarenOpvullen(viewModel, id);
            return View(viewModel);
        }

        //CRUD Mede-Eigenaren

        public async Task<IActionResult> CreateMedeEigenaarObject(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }
            MedeEigenaar medeEigenaar = await _context.MedeEigenaaren.FirstOrDefaultAsync(x => x.MedeEigenaarID == id);
            if (medeEigenaar == null)
            {
                return NotFound();
            }
            else
            {
                MedeEigenaarObject medeEigenaarObject = new MedeEigenaarObject()
                {
                    MedeEigenaarID = medeEigenaar.MedeEigenaarID,
                    ObjectID = (int)id2
                };
                //in dit geval geen crash als er snel wordt geklikt op hetzelfde medeEigenaarObject
                MedeEigenaarObject testMedeEigenaarObject = await _context.MedeEigenaarObjecten.FirstOrDefaultAsync(x => x == medeEigenaarObject);
                if (testMedeEigenaarObject == null)
                {
                    _context.MedeEigenaarObjecten.Add(medeEigenaarObject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("EditVerzamelObject", new { id = id2 });
                }              
            }
            return RedirectToAction("EditVerzamelObject", new { id = id2});
        }

      
        public async Task<IActionResult> DeleteMedeEigenaarObject(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }
            MedeEigenaarObject medeEigenaarObject = await _context.MedeEigenaarObjecten.FirstOrDefaultAsync(x => x.MedeEigenaarID == id);
            //in dit geval geen crash als er snel wordt geklikt op hetzelfde verzamelobject als de onclick zou verwijderd worden
            if (medeEigenaarObject != null)
            {
                _context.MedeEigenaarObjecten.Remove(medeEigenaarObject);
                await _context.SaveChangesAsync();
                return RedirectToAction("EditVerzamelObject", new { id = id2 });
            }          
            return RedirectToAction("EditVerzamelObject", new { id = id2 });
        }




        //Delete VerzamelObject



        public async Task<IActionResult> DeleteVerzamelObject(int? id)
        {
            if (id != null)
            {
                VerzamelObject verzamelObject = await _context.VerzamelObjecten.FirstOrDefaultAsync(x => x.ID == id);
                if (verzamelObject == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.VerzamelObjecten.Remove(verzamelObject);
                    await _context.SaveChangesAsync();
                }               
            }
            return RedirectToAction(nameof(VerzamelObjectOverzicht));

        }

        //hulper functies

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

        private string UploadedFileExisting(EditVerzamelObjectViewModel ViewModel, string oldImage)
        {
            string uniqueFileName;
            if (ViewModel.NieuweAfbeelding != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                if(oldImage != null)
                {
                    string oldFilePath = Path.Combine(uploadsFolder, oldImage);
                    System.IO.File.Delete(oldFilePath);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ViewModel.NieuweAfbeelding.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ViewModel.NieuweAfbeelding.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = oldImage;
            }
            return uniqueFileName;
        }
    }
}
