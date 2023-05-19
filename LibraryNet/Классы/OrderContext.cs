using LibraryNet.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryNet.Классы
{
    public class OrderContext : DbContext
    {
        public OrderContext()
            : base("DBConnection")
        { }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public NakladFactory factory;
        public bool Delete_Tovar(OrderItem tovar)
        {
            if (tovar == null) return false;
            tovar.Order.OrderItems.Remove(tovar);
            OrderItem.Remove(tovar);
            SaveChanges();
            return true;
        }
        public bool Clear_Tovars(int number)
        {
            var tovar = OrderItem.FirstOrDefault(id => id.FK == number);
            if (tovar == null) return false;
            else
                while (tovar != null)
                {
                    OrderItem.Remove(tovar);
                    SaveChanges();
                    tovar = OrderItem.FirstOrDefault(id => id.FK == number);
                }
            return true;
        }
        public bool Delete_Naklad(Order order)
        {
            Clear_Tovars(order.Number);
            Order.Remove(order);
            SaveChanges();
            return true;
        }
        public bool Order_Add(int number)
        {
            if (Order.FirstOrDefault(id => id.Number == number) != null) return false;

            IOrder order = factory.CreateOrder(number);//Создаем накладную

            Order.Add((Order)order);
            SaveChanges();
            return true;
        }
        public OrderItem Tovar_Create(Order order)
        {
            IOrderItem tovar = factory.CreateOrderItem(order);
            order.OrderItems.Add((OrderItem)tovar);
            OrderItem.Add((OrderItem)tovar);
            SaveChanges();
            return (OrderItem)tovar;
        }
        public List<Order> SearchNakladInSum(decimal sum)
        {
            return Order.ToList().FindAll(Sum => Sum.Sum >= sum);
        }
    }
}
