
namespace task.Repositories.ProductRepository
{
    public interface IProductRepository
    {
    
        ICollection<ProductInfoDTO> GetAll();

        Product GetbyId(int Id);

        void Insert(CreateProductDTO item);
        bool Update(int Id, UpdateProductDTO Item);

        bool Delete(int Id);

        // search 
        ICollection<ProductInfoDTO> Search(string search);

    }
}
