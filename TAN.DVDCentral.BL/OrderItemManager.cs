using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAN.DVDCentral.BL.Models;
using TAN.DVDCentral.PL;

namespace TAN.DVDCentral.BL
{
    public class OrderItemManager
    {
        public static int Insert(int orderId, int quantity, int movieId, double cost, ref int id, bool rollback = false)
        {
            try
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = orderId,
                    Quantity = quantity,
                    MovieId = movieId,
                    Cost = cost
                };

                int results = Insert(orderItem, rollback);

                //Backfill
                id = orderItem.Id;
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = new tblOrderItem();
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(oi => oi.Id) + 1 : 1;
                    entity.OrderId = orderItem.OrderId;
                    entity.Quantity = orderItem.Quantity;
                    entity.MovieId = orderItem.MovieId;
                    entity.Cost = orderItem.Cost;

                    //Back fill
                    orderItem.Id = entity.Id;

                    dc.tblOrderItems.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }

                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static int Update(OrderItem movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.OrderId = movie.OrderId;
                        entity.Quantity = movie.Quantity;
                        entity.MovieId = movie.MovieId;
                        entity.Cost = movie.Cost;

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
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrderItems.Remove(entity);
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

        public static OrderItem LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == id);

                    if (entity != null)
                    {
                        return new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId, 
                            Quantity = entity.Quantity,
                            MovieId = entity.MovieId,
                            Cost = entity.Cost
                        };
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

        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from oi in dc.tblOrderItems
                     select new
                     {
                         oi.Id,
                         oi.OrderId,
                         oi.Quantity,
                         oi.MovieId,
                         oi.Cost
                     })
                     .ToList()
                    .ForEach(orderItem => list.Add(new Models.OrderItem
                    {
                        Id = orderItem.Id,
                        OrderId= orderItem.OrderId,
                        Quantity = orderItem.Quantity,
                        MovieId = orderItem.MovieId,
                        Cost = orderItem.Cost
                    }));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<OrderItem> LoadByOrderId(int? orderId = null)
        {
            try
            {
                List<OrderItem> orderItems = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from oi in dc.tblOrderItems
                     join o in dc.tblOrders on oi.OrderId equals o.Id
                     where oi.OrderId == orderId || orderId == null
                     select new
                     {
                         o.Id,
                         oi.OrderId,
                         oi.Quantity,
                         oi.MovieId,
                         oi.Cost
                     }).ToList()

                     .ForEach(item => orderItems.Add(new OrderItem
                     {
                         Id = item.Id,
                         OrderId= item.OrderId,
                         Quantity = item.Quantity,
                         MovieId = item.MovieId,
                         Cost = item.Cost

                     }));
                }
                return orderItems;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
