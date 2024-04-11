using BlazorBootcamp_Models;
using BlazorBootcampWeb_Client.Service.IService;
using Newtonsoft.Json;

namespace BlazorBootcampWeb_Client.Service
{
    public class ProductService : IProductService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDTO> Get(int productId)
        {
            var responese = await _httpClient.GetAsync($"/api/product/{productId}");
            var content = await responese.Content.ReadAsStringAsync();

            if (responese.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var responese = await _httpClient.GetAsync("/api/product");
            if(responese.IsSuccessStatusCode)
            {
                var content = await responese.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                
                foreach(var prod in products)
                {
                    prod.ImageUrl = BaseServerUrl + prod.ImageUrl;
                }
                
                return products;
            }

            return new List<ProductDTO>();
        }
    }
}
