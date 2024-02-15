namespace Model
{

    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public ProductModel(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

    }

}
