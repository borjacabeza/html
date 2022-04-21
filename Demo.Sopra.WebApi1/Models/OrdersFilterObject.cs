using System;

namespace Demo.Sopra.WebApi1.Models
{
    public class OrdersFilterObject
    {
        public string CustomerID { get; set; }
        public int EmployeedID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }
}