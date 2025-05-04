using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdAgencyWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } // Submitted, Confirmed, Completed

        // Навігаційні властивості
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}