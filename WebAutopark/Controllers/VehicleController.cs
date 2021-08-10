using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public VehicleController(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            await _vehicleRepository.Create(vehicle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);

            return View(vehicle);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Vehicle vehicle)
        {
            await _vehicleRepository.Update(vehicle);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
