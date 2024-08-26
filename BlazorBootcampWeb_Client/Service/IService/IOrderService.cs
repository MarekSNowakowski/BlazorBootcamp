using BlazorBootcamp_Models;

namespace BlazorBootcampWeb_Client.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId);
        public Task<OrderDTO> Get(int orderId);

        public Task<OrderDTO> Create(StripePaymentDTO paymentDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccesful(OrderHeaderDTO orderHeaderDTO);
    }
}
