using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using E_Med_App.Models;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok("Order added successfully.");
        }

        [HttpPost]
        [Route("AddOrderDetail")]
        public IActionResult AddOrderDetail([FromBody] OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
            return Ok("Order detail added successfully.");
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        [HttpGet]
        [Route("GetOrderDetails")]
        public IActionResult GetOrderDetails()
        {
            var orderDetails = _context.OrderDetails.ToList();
            return Ok(orderDetails);
        }
    }
}
