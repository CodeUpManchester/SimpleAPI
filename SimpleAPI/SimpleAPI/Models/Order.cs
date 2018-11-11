using System;

namespace SimpleAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
    }

    public enum OrderFields
    {
        OrderId = 0,
        OrderDate,
        RequiredDate,
        ShippedDate
    }
}