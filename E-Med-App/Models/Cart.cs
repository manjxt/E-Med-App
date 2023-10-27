using E_Med_App.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Med_App.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string MedName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        public int UserId { get; set; }
    }

}