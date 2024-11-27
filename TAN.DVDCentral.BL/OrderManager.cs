using Humanizer;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAN.DVDCentral.BL.Models;
using TAN.DVDCentral.PL;

namespace TAN.DVDCentral.BL
{
    public class OrderManager
    {
        public static int Insert(int customerId, DateTime orderDate, int userId, DateTime shipDate, List<OrderItem> orderItems, ref int id, bool rollback = false)
        {
            try
            {
        
                Order order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = orderDate,
                    UserId = userId,
                    ShipDate = shipDate,
                    OrderItems = orderItems,

                };

                int results = Insert(order, rollback);

                //Backfill
                id = order.Id;
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = new tblOrder();
                    entity.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(g => g.Id) + 1 : 1;
                    entity.CustomerId = order.CustomerId;
                    entity.OrderDate = order.OrderDate;
                    entity.UserId = order.UserId;
                    entity.ShipDate = order.ShipDate;

                    //Back fill
                    order.Id = entity.Id;

                    dc.tblOrders.Add(entity);
                    results = dc.SaveChanges();

                    foreach (var item in order.OrderItems)
                    {
                        tblOrderItem orderItemEntity = new tblOrderItem();
                        orderItemEntity.Id += dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(oi => oi.Id) + 1 : 1;
                        orderItemEntity.OrderId = entity.Id;
                        orderItemEntity.MovieId = item.MovieId;
                        orderItemEntity.Cost = item.Cost;
                        orderItemEntity.Quantity = item.Quantity;

                        //Back fill
                       item.Id = orderItemEntity.Id;

                        dc.tblOrderItems.Add(orderItemEntity);
                        results += dc.SaveChanges();
                    }
                   
                    if (rollback) transaction.Rollback();
                }
                
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblOrder entity = dc.tblOrders.FirstOrDefault(g => g.Id == order.Id);

                    if (entity != null)
                    {
                        entity.CustomerId = order.CustomerId;
                        entity.OrderDate = order.OrderDate;
                        entity.UserId = order.UserId;
                        entity.ShipDate = order.ShipDate;

                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get row
                    tblOrder entity = dc.tblOrders.FirstOrDefault(g => g.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrders.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Order> Load()
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from o in dc.tblOrders
                     select new
                     {
                         o.Id,
                         o.CustomerId,
                         o.UserId,
                         o.OrderDate,
                         o.ShipDate
                         
                     })
                     .ToList()
                    .ForEach(rating => list.Add(new Models.Order
                    {
                        Id = rating.Id,
                        CustomerId = rating.CustomerId,
                        UserId = rating.UserId,
                        OrderDate = rating.OrderDate,
                        ShipDate = rating.ShipDate

                    }));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Order LoadById(int? orderId = null)
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrder order = dc.tblOrders.FirstOrDefault(o => o.Id == orderId);

                    if (order != null)
                    {
                        Order loadedOrder = new Order
                        {
                            Id = order.Id,
                            CustomerId = order.CustomerId,
                            OrderDate = order.OrderDate,
                            UserId = order.UserId,
                            ShipDate = order.ShipDate,
                            OrderItems = new List<OrderItem>()
                        };

                        List<tblOrderItem> orderItems = dc.tblOrderItems.Where(oi => oi.OrderId == orderId).ToList();
                        foreach (tblOrderItem oi in orderItems)
                        {
                            OrderItem orderItem = new OrderItem
                            {
                                Id = oi.Id,
                                OrderId = oi.OrderId,
                                MovieId = oi.MovieId,
                                Quantity = oi.Quantity,
                                Cost = oi.Cost
                            };

                            loadedOrder.OrderItems.Add(orderItem);
                        }

                        return loadedOrder;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Order> Load(int? CustomerId = null)
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from o in dc.tblOrders
                     join c in dc.tblCustomers on o.CustomerId equals c.Id
                     join u in dc.tblUsers on o.UserId equals u.Id
                     where o.CustomerId == CustomerId || CustomerId == null
                     select new
                     {
                         o.Id,
                         o.CustomerId,
                         o.OrderDate,
                         o.UserId,
                         o.ShipDate
                     })
                     .ToList()
                    .ForEach(order => list.Add(new Models.Order
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        OrderDate = order.OrderDate,
                        UserId = order.UserId,
                        ShipDate = order.ShipDate,
                    }));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
