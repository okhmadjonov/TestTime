using TestTime.Models;

namespace TestTime.Dto
{
    public class RoleAndProductDto
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Product> Products { get; set; }
    }
}
