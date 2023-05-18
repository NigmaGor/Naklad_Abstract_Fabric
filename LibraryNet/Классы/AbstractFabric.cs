using LibraryNet.Domain;

namespace LibraryNet.Классы
{
    public class AbstractFabric
    {
        public interface IOrderFactory
        {
            Order CreateOrder(int number);
        }
        public interface IOrderItemFactory
        {
            OrderItem CreateOrderItem(Order order);
        }
        public class NakladOrderFactory : IOrderFactory
        {
            public Order CreateOrder(int number)
            {
                return new Order { Number = number };
            }
        }
        public class NakladOrderItemFactory : IOrderItemFactory
        {
            public OrderItem CreateOrderItem(Order order)
            {
                return new OrderItem { FK = order.Number, Order = order };
            }
        }

    }
}

