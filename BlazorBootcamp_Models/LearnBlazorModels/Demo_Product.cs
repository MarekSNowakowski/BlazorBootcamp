namespace BlazorBootcamp_Models.LearnBlazorModels
{
    public class Demo_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public List<Demo_ProductProp> Properties { get; set; }
    }
}
