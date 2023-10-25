using E_Med_App.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*namespace E_Med_App.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}

namespace E_Med_App.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public Medicine Medicine { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}*/

namespace E_Med_App.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int MedId { get; set; }
        [ForeignKey("MedId")]
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}