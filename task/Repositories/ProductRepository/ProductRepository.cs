using Microsoft.AspNetCore.Mvc;

namespace task.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        // Deal with database , we need instance from dbcontext
        private ApplicationDbContext _context;

        // Inject _context inside constructor of thid controller to can use it
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<ProductInfoDTO> GetAll()
        {
            List<ProductInfoDTO> ProductsInfo = new List<ProductInfoDTO>();
            var products = this._context.products.ToList();
            foreach (var item in products)
            {
                ProductInfoDTO dto = new ProductInfoDTO()
                {
                    //Id = item.Id,
                    Name = item.ProductName,
                    Quantity = item.ProductQuantity,
                    Price = item.ProductPrice,
                    Photo = item.ProductPhoto
                };

                ProductsInfo.Add(dto);
            }
            return ProductsInfo;
        }

        public Product GetbyId(int Id)
        {
            return this._context.products.FirstOrDefault(p => p.Id == Id);
        }


        void IProductRepository.Insert([FromBody]CreateProductDTO item)
        {
            // string uploading = Path.Combine(Directory.GetCurrentDirectory(), "images");
            //string unique = Guid.NewGuid().ToString() + item.Photo.FileName;
            // string pathfile = Path.Combine(uploading, unique);  
            // using(var filestream = new FileStream(pathfile, FileMode.Create))
            //{
            //   item.Photo.CopyTo(filestream);
            //  filestream.Close();
            //}

            var product = new Product
              {

              ProductName = item.Name,
             ProductQuantity = item.Quantity,
             ProductPrice = item.Price,
            ProductPhoto = item.Photo 
            };
            this._context.products.Add(product);
            _context.SaveChanges();

        }

        bool IProductRepository.Update(int Id, UpdateProductDTO Item)
        {
            var product = new Product();
            product = GetbyId(Id);

            if (product != null)
            {

                product.ProductName = Item.Name;
                product.ProductQuantity = Item.Quantity;
                product.ProductPrice = Item.Price;
                product.ProductPhoto = Item.Photo;
                //if(Item.Photo!=null)
                //{
                //    string uploading = Path.Combine(Directory.GetCurrentDirectory(), "images");
                //    string unique = Guid.NewGuid().ToString() + "_" + Item.Photo.FileName;
                //    string pathfile = Path.Combine(uploading, unique);
                //    using (var filestream = new FileStream(pathfile, FileMode.Create))
                //    {
                //        Item.Photo.CopyTo(filestream);
                //        filestream.Close();
                //    }
                //    product.ProductPhoto = unique;
                //}
                _context.SaveChanges();
                return true;
            }
            return false;

        }


        bool IProductRepository.Delete(int Id)
        {
            var product = GetbyId(Id);
            if (product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;


        }


        //search 
        public ICollection<ProductInfoDTO> Search(string search)
        {

            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentException("Search term cannot be empty.");
            }

            var products = _context.products
                .Where(p => p.ProductName.Contains(search) || p.ProductPrice.ToString().Contains(search))
                .ToList();

            List<ProductInfoDTO> ProductsInfo = new List<ProductInfoDTO>();
            foreach (var item in products)
            {
                ProductInfoDTO dto = new ProductInfoDTO()
                {
                    //Id = item.Id,
                    Name = item.ProductName,
                    Quantity = item.ProductQuantity,
                    Price = item.ProductPrice,
                    Photo = item.ProductPhoto 
                };

                ProductsInfo.Add(dto);
            }
            return ProductsInfo;
        }
    }
}

