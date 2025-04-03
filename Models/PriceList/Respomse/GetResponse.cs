namespace price.list.Models.PriceList.Respomse
{
    public class GetResponse
    {
        public List<Product>? ProductList { get; set; }
    }

    public class Product
    {
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? UrlImage { get; set; }
    }
}
