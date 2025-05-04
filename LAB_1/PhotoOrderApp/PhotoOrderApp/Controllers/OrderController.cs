using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

public class OrderController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(PhotoOrder order)
    {
        if (ModelState.IsValid)
        {
            decimal total = order.CalculateTotal();
            string email = TempData["UserEmail"]?.ToString();

            if (!string.IsNullOrEmpty(email))
            {
                SendEmail("admin@example.com", "Нове замовлення", $"Email: {email}\nСума: {total} грн");
            }

            ViewBag.Total = total;
            return View("Result", order);
        }

        return View(order);
    }

    private void SendEmail(string to, string subject, string body)
    {
        var mail = new MailMessage("your@gmail.com", to, subject, body);
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("artemus200918@gmail.com", "eucbyillixzceent"),
            EnableSsl = true
        };
        client.Send(mail);
    }
}