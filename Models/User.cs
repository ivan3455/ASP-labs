namespace PizzaApp.Models
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public List<Product> Products { get; set; }
    }
}