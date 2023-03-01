namespace task.Models
{

    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MinLength(3)]  //  nullable & more than 3 letters
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPrice { get; set; }
        public string ProductPhoto { get; set; }

      
    }
}
