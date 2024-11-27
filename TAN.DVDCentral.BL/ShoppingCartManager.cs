using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TAN.DVDCentral.BL.Models;

namespace TAN.DVDCentral.BL
{
    public class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Movie movie)
        {
            if (cart != null)
            {
                cart.movieItems.Add(movie);
            }
        }

        public static void Remove(ShoppingCart cart, Movie movie)
        {
            if (cart != null)
            {
                cart.movieItems.Remove(movie);
            }
        }
        public static void Checkout(ShoppingCart cart)
        {
            // Make a new order
            Order order = new Order();
            DateTime today = DateTime.Now;
            //Set the order fields as needed
            order.Id = 5;
            order.OrderDate = today;
            order.ShipDate = today.AddDays(3);
            order.CustomerId = 1;
            order.UserId = 1;

            order.OrderItems = new List<OrderItem>();

            foreach (Movie movie in cart.movieItems)
            {
                //Make a new orderItem
                OrderItem orderItem = new OrderItem();
                orderItem.MovieId = movie.Id;
                orderItem.Cost = movie.Cost;
                orderItem.OrderId = order.Id;
                orderItem.Quantity = 1;

                order.OrderItems.Add(orderItem);
            }

            //Set the orderItem fields from the item
            //order.OrderItems.Add(orderItem)

            OrderManager.Insert(order);

            //Decrement the tblMovie.InstkQty appropriately
            cart = new ShoppingCart();
        }
    }
}
