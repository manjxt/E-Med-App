using System.ComponentModel.DataAnnotations;

namespace E_Med_App.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Additional user properties can be added here as needed
    }
}
