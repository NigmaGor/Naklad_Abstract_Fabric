using LibraryNet.Классы;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibraryNet.Domain
{
    public class Order : IOrder
    {
        private List<OrderItem> orderItems = new List<OrderItem>();
        private int id;
        private int number;
        private DateTime data;
        public decimal Sum
        {
            get { return orderItems.Sum(s => s.Sum_Nds); }
        }
        public List<OrderItem> OrderItems
        {
            get { return orderItems; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public Order()
        {
            Data = DateTime.Now;
        }
        [Key]
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public DateTime Data { get { return data; } set { data = value; } }
    }
}
