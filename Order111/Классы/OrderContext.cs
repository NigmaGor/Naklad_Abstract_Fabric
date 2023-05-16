using OrderGen.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Order111.Классы.AbstractFabric;

namespace Order111.Классы
{
    
    class OrderContext : DbContext 
    {
        public OrderContext()
            : base("DBConnection")
        { }

        public DbSet<Order> Order { get; set; }  
        public DbSet<OrderItem> OrderItem { get; set; }
        public INakladFactory factory;

        public bool Delete_Tovar(OrderItem tovar)
        {
            if(tovar == null) return false;
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

            Order order = factory.CreateOrderFactory().CreateOrder(number);//Создаем накладную

            Order.Add(order);
            SaveChanges();
            return true;
        }
        public OrderItem Tovar_Create(Order order)
        {
           var tovar = factory.CreateOrderItemFactory().CreateOrderItem(order);
           order.OrderItems.Add(tovar);
           OrderItem.Add(tovar);
           SaveChanges();
           return tovar;
        }
        public List<Order> SearchNakladInSum(decimal sum)
        {
            return Order.ToList().FindAll(Sum => Sum.Sum >= sum);
        }
    }
}
