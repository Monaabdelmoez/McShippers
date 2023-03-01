using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task.ApplicationDTO;
namespace task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {  
       //DIP
       IProductRepository ProductRepository;

       //DI dependance injection
        public ProductsController(IProductRepository _productrepo)
        {
            ProductRepository =_productrepo;
        }

        [HttpGet]
        public  IActionResult  GetAll()
        {
           var products = ProductRepository.GetAll();
            return Ok(products);
        }

        [HttpPost] 
        // create new product in database
        public IActionResult Create(CreateProductDTO productDTO )
        {
            if (ModelState.IsValid)
            {

                ProductRepository.Insert(productDTO);
                return Ok(new { message = "Product added" });

            }
            return BadRequest(ModelState);


        }


        [HttpPut("{id}")]
        // update product at database

        public IActionResult  Update(int id, UpdateProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Update(id, productDTO);
                return Ok(new { message = "Product updated" });
            }

            return BadRequest(ModelState);

        }



        [HttpDelete("{id}")] 
        //Delete product from database
        public IActionResult Delete(int id)
        {          
                ProductRepository.Delete(id);
                return Ok(new { message = "Product deleted" });           

        }



        [HttpGet("Search")]
        public IActionResult Search(string search)
        {
            var products = ProductRepository.Search(search);

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }




    }
}
