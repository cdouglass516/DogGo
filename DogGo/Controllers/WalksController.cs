using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;


        public WalksController(IWalksRepository WalksRepository,
                                IDogRepository dogRepository,
                                IWalkerRepository walkerRepository)
        {
            _walksRepo = WalksRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
        }
        public IActionResult Index()
        {
            List<WalksViewModel> walks = _walksRepo.GetWalks();
            return View(walks);
        }
        public IActionResult Details(int id)
        {
            WalksViewModel vm = _walksRepo.GetWalkById(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkersController/Create
        public IActionResult Create()
        {
            //Owner o = new();
            //o.Neighborhoods = _ownerRepo.GetNeighborhoods();
            //return View(o);
            return NotFound();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Owner owner)
        {
            try
            {
                //_ownerRepo.AddOwner(owner);
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
            WalksViewModel vm = _walksRepo.GetWalkById(id);
            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
            
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Owner collection)
        {
            try
            {
                //_ownerRepo.Edit(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //return View();
                return NotFound();
            }
        }

        // GET: WalkersController/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                //_ownerRepo.Delete(id);
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
    }
}
