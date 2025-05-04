using System.ComponentModel.DataAnnotations;

namespace AdAgencyWebApp.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва послуги обов'язкова")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, 1000000, ErrorMessage = "Ціна має бути позитивним числом")]
        public decimal Price { get; set; }
    }
}