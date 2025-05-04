using System;

namespace ProductLinqApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Price { get; set; }
    }
}