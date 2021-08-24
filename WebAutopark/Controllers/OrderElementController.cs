using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class OrderElementController : Controller
    {
        private readonly IRepository<OrderElement> _orderElementRepository;
        private readonly IRepository<Detail> _detailRepository;

        public OrderElementController(IRepository<OrderElement> orderElementRepository,
            IRepository<Detail> detailRepository)
        {
            _orderElementRepository = orderElementRepository;
            _detailRepository = detailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(Order order)
        {
            var details = new SelectList(await _detailRepository.GetAll(), "DetailId", "Name");
            ViewBag.Details = details;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderElement orderElement)
        {
            if(ModelState.IsValid)
            {
                await _orderElementRepository.Create(orderElement);

                return RedirectToAction("Details", "Order", new { id = orderElement.OrderId });
            }

            return View();
        }
    }
}
