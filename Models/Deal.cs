using System;

namespace CarShop.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public int VehicleId { get; set; }
        public int Price { get; set; }
    }
}
