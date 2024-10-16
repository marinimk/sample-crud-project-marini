using System.ComponentModel.DataAnnotations;

namespace CRUDApp.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
        public  string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^01\d{8}$|^011\d{8}$", ErrorMessage = "Phone number must be 10 digits starting with 01 or 11 digits starting with 011.")]
        public  string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email address")]
        public  string Email { get; set; }

        public bool Member { get; set; }
    }
}
