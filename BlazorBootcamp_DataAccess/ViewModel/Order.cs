namespace BlazorBootcamp_DataAccess.ViewModel
{
    public class Order
    {
        public OrderHeader Header { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }
    }
}
