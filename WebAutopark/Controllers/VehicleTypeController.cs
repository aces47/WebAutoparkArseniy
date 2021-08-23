using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleTypeController(IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            var vehicleTypes = (await _vehicleTypeRepository.GetAll());

            return View(vehicleTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            if(ModelState.IsValid)
            {
                await _vehicleTypeRepository.Create(vehicleType);
                return RedirectToAction("Index");
            }

            return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var vehicleType = await _vehicleTypeRepository.GetById(id);

            return View(vehicleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypeRepository.Update(vehicleType);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleTypeRepository.Delete(id);

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var vehicleType = await _vehicleTypeRepository.GetById(id);

            return View(vehicleType);
        }
    }
}
