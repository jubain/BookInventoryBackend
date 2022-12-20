using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Asp_Dot_Net_Web_Api.Dtos;
using Asp_Dot_Net_Web_Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/values
        [HttpGet]
        public object Get()
        {
            var orders = _db.Order;
            if (orders.Count() == 0)
            {
                return NotFound("No orders found");
            }
            return Ok(orders);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var order = _db.Order.Find(id);
            if (order == null)
            {
                return NotFound("No order found");
            }
            return Ok(order);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto order)
        {
            try
            {
                var user = _db.User.Find(order.userId);
                if (user == null) return NotFound("User not found!");


                foreach (BookObject o in order.Books)
                {
                    var book = _db.Book.Find(o.BookId);
                    if (book?.quantity < o.quantity)
                    {
                        throw new Exception("Sorry, The book quantity is more than the stock!");
                    }
                }

                var newUserOrder = new Order
                {
                    UserId = order.userId,
                };
                _db.Order.Add(newUserOrder);
                _db.SaveChanges();


                foreach (BookObject o in order.Books)
                {
                    var book = _db.Book.Find(o.BookId);
                    if (book != null)
                    {
                        var newBookOrder = new BookOrder { BookId = o.BookId, quantity = o.quantity, price = book.price * o.quantity, OrderId = newUserOrder.id };
                        _db.BookOrders.Add(newBookOrder);

                    }
                }
                _db.SaveChanges();

                return Ok(order);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        // PATCH api/values/5
        [HttpPatch("{id}")]
        public object Patch(int id, [FromBody] JsonPatchDocument<Order> order)
        {
            var userOrder = _db.Order.Find(id);
            if (userOrder == null)
            {
                return NotFound("Sorry, Order not found!");
            }
            order.ApplyTo(userOrder);
            return userOrder;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

