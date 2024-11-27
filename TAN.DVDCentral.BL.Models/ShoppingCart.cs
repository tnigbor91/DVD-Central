using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAN.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        OrderItem orderItem;

        public List<OrderItem> orderItems = new List<OrderItem>();

        public List<Movie> movieItems = new List<Movie>();
        public int NumberOfItems { get { return movieItems.Count; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal
        {
            get
            {
                double subtotal = 0;
                foreach (var item in movieItems)
                {
                    subtotal += item.Cost;
                }
                return subtotal;
            }
        }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * .055; } }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }
    }

}
