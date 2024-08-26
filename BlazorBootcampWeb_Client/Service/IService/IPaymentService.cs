using BlazorBootcamp_Models;
using BlazorBootcampWeb_Client.ViewModels;

namespace BlazorBootcampWeb_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO payment);
    }
}
