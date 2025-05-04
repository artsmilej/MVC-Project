using Microsoft.AspNetCore.Mvc;
using ProductLinqApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductLinqApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = GetSampleProducts();

            var cheapest = products.OrderBy(p => p.Price).First();
            var mostExpensive = products.OrderByDescending(p => p.Price).First();
            var averagePrice = products.Average(p => p.Price);

            ViewBag.Cheapest = cheapest;
            ViewBag.MostExpensive = mostExpensive;
            ViewBag.AveragePrice = averagePrice;

            return View(products);
        }

        private List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Молоко", ExpirationDate = DateTime.Today.AddDays(5), Price = 25 },
                new Product { Name = "Сир", ExpirationDate = DateTime.Today.AddDays(20), Price = 80 },
                new Product { Name = "Хліб", ExpirationDate = DateTime.Today.AddDays(3), Price = 18 },
                new Product { Name = "Масло", ExpirationDate = DateTime.Today.AddDays(10), Price = 60 },
                new Product { Name = "Ковбаса", ExpirationDate = DateTime.Today.AddDays(15), Price = 95 },
                new Product { Name = "Йогурт", ExpirationDate = DateTime.Today.AddDays(7), Price = 22 },
                new Product { Name = "Шоколад", ExpirationDate = DateTime.Today.AddMonths(6), Price = 35 },
                new Product { Name = "Кава", ExpirationDate = DateTime.Today.AddYears(1), Price = 110 },
                new Product { Name = "Цукор", ExpirationDate = DateTime.Today.AddYears(2), Price = 40 },
                new Product { Name = "Сіль", ExpirationDate = DateTime.Today.AddYears(3), Price = 15 }
            };
        }
    }
}