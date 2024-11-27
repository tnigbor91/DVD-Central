using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAN.DVDCentral.BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        [DisplayName("Ship Date")]
        public DateTime ShipDate { get; set; }

        [DisplayName("Order Items")]
        public List<OrderItem>? OrderItems { get; set;}

        [DisplayName("Total Cost")]
        public double TotalCost { get; set; }

    }
}
