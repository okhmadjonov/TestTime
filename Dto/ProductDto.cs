using System.ComponentModel.DataAnnotations;

namespace TestTime.Dto
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Title must contain only letters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number with up to two decimal places")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Quantity must contain only numbers")]
        public int Quantity { get; set; }

    }
}
