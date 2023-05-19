using LibraryNet.Domain;
using System.Collections.Generic;
using System;

namespace LibraryNet.Классы
{
    public interface IOrder
    {
        DateTime Data { get; set; }
        int ID { get; set; }
        int Number { get; set; }
        List<OrderItem> OrderItems { get; }
        decimal Sum { get; }
    }
    public interface IOrderItem
    {
        decimal Count { get; set; }
        int FK { get; set; }
        int ID { get; set; }
        decimal Nds { get; set; }
        Order Order { get; set; }
        decimal Price { get; set; }
        decimal Rubl_Nds { get; set; }
        string Sepecification { get; set; }
        decimal Sum_Nds { get; set; }
        decimal Total_Sum { get; set; }
    }
        public interface INakladFactory
        {
            IOrder CreateOrder(int number);
            IOrderItem CreateOrderItem(Order order);
        }
        public class NakladFactory : INakladFactory
        {
            public IOrder CreateOrder(int number)
            {
                return new Order { Number = number };
            }
            public IOrderItem CreateOrderItem(Order order)
            {
                return new OrderItem { FK = order.Number, Order = order };
            }
        }
    }

