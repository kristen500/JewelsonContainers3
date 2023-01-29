using WebMVC.Models.OrderModels;

namespace WebMVC.services
{
    public interface IOrderService
    {
        Task<Order> GetOrder(string orderId);
        Task<int> CreateOrder(Order order);

    }
}
