namespace TestTask.DataAccessLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int CategoryId { get; set; }
        public int Price { get; set; }
    }
}
