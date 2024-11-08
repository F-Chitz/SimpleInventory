using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRespository<Product> _productRepository;

        public ProductController(IBaseRespository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getactives")]
        public async Task<IEnumerable<Product>> GetAllActives()
        {
            return await _productRepository.GetAll();
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll(true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            Product product = await _productRepository.GetById(id);

            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public ActionResult<Product> Add(Product product)
        {
            return _productRepository.Add(product) ? Ok(product) : BadRequest();
        }

        [HttpPut]
        public ActionResult<Product> Update(Product product)
        {
            return _productRepository.Update(product) ? Ok(product) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productRepository.GetById(id);

            if (product != null)
            {
                _productRepository.Delete(product);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
