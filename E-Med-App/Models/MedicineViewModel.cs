namespace E_Med_App.Models
{
    public class MedicineViewModel
    {
        public List<Medicine> Medicines{ get; set; }
        public Dictionary<int, int> MedicineQuantities { get; set; } // LaptopId -> Quantity
    }
}
