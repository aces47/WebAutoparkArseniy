using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class DetailController : Controller
    {
        private readonly IRepository<Detail> _detailRepository;

        public DetailController(IRepository<Detail> detailRepository)
        {
            _detailRepository = detailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var details = (await _detailRepository.GetAll()).OrderBy(e => e.DetailId);

            return View(details);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Detail detail)
        {
            if (ModelState.IsValid)
            {
                await _detailRepository.Create(detail);
                return RedirectToAction("Index");
            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var detail = await _detailRepository.GetById(id);

            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Detail detail)
        {
            if (ModelState.IsValid)
            {
                await _detailRepository.Update(detail);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _detailRepository.Delete(id);

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var detail = await _detailRepository.GetById(id);

            return View(detail);
        }
    }
}
