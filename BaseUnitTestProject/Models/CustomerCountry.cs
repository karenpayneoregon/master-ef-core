namespace BaseUnitTestProject.Models
{
    public class CustomerCountry
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString() => $"{Id,-3}{Name,-40}  {Country}";

    }
}