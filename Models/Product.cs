namespace TestTime.Models
{
    public class Product
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public double TotalPrice { get; set; } 

    }
}
