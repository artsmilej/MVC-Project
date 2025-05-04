using System.ComponentModel.DataAnnotations;

namespace AdAgencyWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email обов'язковий")]
        [EmailAddress(ErrorMessage = "Невірний формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; } // Admin або Client
    }
}