namespace task.ApplicationDTO
{
    public class UpdateProductDTO
    {
        
        [Required, MinLength(3)]  // nullable & more than 3 letters
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string  Photo { get; set; }
    }
}
