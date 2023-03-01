using task.Models;
namespace task.ApplicationDTO
{
    public class CreateProductDTO
    {
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }
        [Required, MinLength(3)]  
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }

    
    }
}

