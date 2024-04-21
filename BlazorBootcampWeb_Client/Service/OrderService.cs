using BlazorBootcamp_Models;
using BlazorBootcampWeb_Client.Service.IService;
using Newtonsoft.Json;

namespace BlazorBootcampWeb_Client.Service
{
    public class OrderService : IOrderService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDTO> Get(int orderHeaderId)
        {
            var responese = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await responese.Content.ReadAsStringAsync();

            if (responese.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null)
        {
            var responese = await _httpClient.GetAsync("/api/order");
            if (responese.IsSuccessStatusCode)
            {
                var content = await responese.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);

                return orders;
            }

            return new List<OrderDTO>();
        }
    }
}
