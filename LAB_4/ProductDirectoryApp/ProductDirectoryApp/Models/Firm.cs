using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductDirectoryApp.Models
{
    public class Firm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва фірми обов’язкова")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Назва фірми повинна містити щонайменше 2 символи")]
        [Display(Name = "Назва фірми")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}