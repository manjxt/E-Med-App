// OrderController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using E_Med_App.Models;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> _orders;
        private readonly List<OrderDetail> _orderDetails;

        public OrderController()
        {
            _orders = new List<Order>();
            _orderDetails = new List<OrderDetail>();
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(Order order)
        {
            _orders.Add(order);
            return Ok("Order added successfully.");
        }

        [HttpPost]
        [Route("AddOrderDetail")]
        public IActionResult AddOrderDetail(OrderDetail orderDetail)
        {
            _orderDetails.Add(orderDetail);
            return Ok("Order detail added successfully.");
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            return Ok(_orders);
        }

        [HttpGet]
        [Route("GetOrderDetails")]
        public IActionResult GetOrderDetails()
        {
            return Ok(_orderDetails);
        }
    }
}
