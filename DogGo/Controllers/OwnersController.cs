using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;


        public OwnersController(IOwnerRepository ownerRepository,
                                IDogRepository dogRepository,
                                IWalkerRepository walkerRepository)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
        }
        public IActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();
            return View(owners);
        }
        public IActionResult Details(int id)
        {
            ProfileViewModel vm = new();
            vm.Owner = _ownerRepo.GetOwnerById(id);
            vm.Dogs = _dogRepo.GetDogsByOwner(id);
            vm.Walkers = _walkerRepo.GetWalkersByNeigborhood(vm.Owner.NeighborhoodId);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkersController/Create
        public IActionResult Create()
        {
            Owner o = new();
            o.Neighborhoods = _ownerRepo.GetNeighborhoods();
            return View(o);
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepo.AddOwner(owner);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(owner);
            }
        }

        // GET: WalkersController/Edit/5
        public IActionResult Edit(int id)
        {
            Owner o = _ownerRepo.GetOwnerById(id);
            return View(o);
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Owner collection)
        {
            try
            {
                _ownerRepo.Edit(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                _ownerRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Walkers/Details/5

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private List<Neighborhood> GetColors()
        {
            List<Neighborhood> neighborhoods = new();

            return neighborhoods;
        }
        public class IndexViewModel
        {
            [Required]
            public string Neighborhood { get; set; }

            [Required]
            public string Value { get; set; }
            public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Neighborhoods { get; set; }
        }
    }
}
