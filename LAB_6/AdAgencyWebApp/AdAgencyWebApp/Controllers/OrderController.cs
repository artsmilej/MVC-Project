using Microsoft.AspNetCore.Mvc;
using AdAgencyWebApp.Models;
using System;
using System.Linq;
using System.Net.Mail;
using AdAgencyWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AdAgencyWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context) => _context = context;

        public IActionResult Create(int serviceId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var order = new Order
            {
                UserId = userId.Value,
                ServiceId = serviceId,
                OrderDate = DateTime.Now,
                Status = "Submitted"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            SendEmail(order);
            return RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Service) // додано!
                .ToList();

            return View(orders);
        }

        private void SendEmail(Order order)
        {
            try
            {
                var service = _context.Services.Find(order.ServiceId);
                var user = _context.Users.Find(order.UserId);
                MailMessage mail = new MailMessage("admin@site.com", user.Email);
                mail.Subject = "Ваше замовлення прийнято";
                mail.Body = $"Дякуємо за замовлення послуги: {service.Name} \nДата: {order.OrderDate}";

                SmtpClient client = new SmtpClient("smtp.yourhost.com")
                {
                    Credentials = new System.Net.NetworkCredential("admin@site.com", "yourpassword"),
                    Port = 587,
                    EnableSsl = true
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка надсилання: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AdminOrders(string status)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin") return RedirectToAction("Login", "Account");

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Service)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            ViewBag.StatusFilter = status;
            return View(orders.ToList());
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin") return Unauthorized();

            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
                TempData["Success"] = $"Статус замовлення змінено на '{status}'";
            }
            return RedirectToAction("AdminOrders");
        }

        [HttpPost]
        public IActionResult Order(int serviceId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var order = new Order
            {
                ServiceId = serviceId,
                UserId = userId.Value,
                OrderDate = DateTime.Now,
                Status = "Submitted"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            TempData["Success"] = "Послугу успішно замовлено!";
            return RedirectToAction("MyOrders");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin") return Unauthorized();

            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                TempData["Success"] = "Замовлення успішно видалено.";
            }

            return RedirectToAction("AdminOrders");
        }


    }
}
