using System.ComponentModel.DataAnnotations;

namespace E_Med_App.Models
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
}

/*
using System.ComponentModel.DataAnnotations;

namespace E_Med_App.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set;}
        public int TotalPrice { get; set; }
    }
}*/