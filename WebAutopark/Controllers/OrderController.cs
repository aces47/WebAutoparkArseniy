using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetById(id);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order item)
        {
            Order order = await _orderRepository.CreateReturn(item);
            
            return RedirectToAction("Create", "OrderElement", order);
        }
    }
}
