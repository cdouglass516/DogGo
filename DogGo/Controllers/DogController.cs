using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;

        public DogController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        public IActionResult Index()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();
            return View(dogs);
        }
        public IActionResult Details(int id)
        {
            Dog owner = _dogRepo.GetDogDetail(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: WalkersController/Create
        //public IActionResult Create()
        //{
        //    Owner o = new();
        //    o.Neighborhoods = _dogRepo.GetNeighborhoods();
        //    return View(o);
        //}

        //// POST: WalkersController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Owner owner)
        //{
        //    try
        //    {
        //        _dogRepo.AddOwner(owner);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View(owner);
        //    }
        //}

        //// GET: WalkersController/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    Owner o = _dogRepo.GetOwnerById(id);
        //    return View(o);
        //}

        //// POST: WalkersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Owner collection)
        //{
        //    try
        //    {
        //        _dogRepo.Edit(collection);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: WalkersController/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        _dogRepo.Delete(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //// GET: Walkers/Details/5

        //// POST: WalkersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
