using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleController(IRepository<Vehicle> vehicleRepository, 
            IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = (await _vehicleRepository.GetAll()).OrderBy(e => e.VehicleId);

            return View(vehicles);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);

            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAll();
            ViewBag.vehicleTypes = new SelectList(vehicleTypes, "VehicleTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehicleRepository.Create(vehicle);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Vehicle vehicle)
        {
            if(ModelState.IsValid)
            {
                await _vehicleRepository.Update(vehicle);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleRepository.Delete(id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);

            return View(vehicle);
        }
    }
}
