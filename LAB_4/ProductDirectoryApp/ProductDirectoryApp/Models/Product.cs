using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDirectoryApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Назва товару")]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Ціна")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Фірма")]
        public int FirmId { get; set; }

        [ForeignKey("FirmId")]
        public Firm Firm { get; set; }
    }
}