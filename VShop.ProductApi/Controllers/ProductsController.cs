using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VShop.ProductApi.DTO;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // retornar todos os produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtosDto = await _productService.GetProducts();
            if (produtosDto == null)
            {
                return NotFound("Products not found");
            }
            return Ok(produtosDto);
        }

        // retornar um produto pelo id
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produtoDto = await _productService.GetProductById(id);
            if (produtoDto == null)
            {
                return NotFound("Product not found");
            }
            return Ok(produtoDto);
        }

        // inserir um produto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Data Invalid");

            await _productService.AddProduct(produtoDto);

            return new CreatedAtRouteResult("GetProduct",
                new { id = produtoDto.Id }, produtoDto);
        }

        // alterar um produto
        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Put([FromBody] ProductDTO produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Data invalid");

            await _productService.UpdateProduct(produtoDto);

            return Ok(produtoDto);
        }

        // deletar um produto
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var produtoDto = await _productService.GetProductById(id);

            if (produtoDto == null)
            {
                return NotFound("Product not found");
            }

            await _productService.RemoveProduct(id);

            return Ok(produtoDto);
        }
    }
}
