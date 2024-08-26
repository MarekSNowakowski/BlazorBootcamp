using BlazorBootcamp_Models;
using BlazorBootcampWeb_Client.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace BlazorBootcampWeb_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessModelDTO> Checkout(StripePaymentDTO payment)
        {
            try
            {
                var content = JsonConvert.SerializeObject(payment);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
                var responseResult = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
