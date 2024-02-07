using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTO;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    //[Route("api/[controller]")]  // pode tirar o "api" e deixar só [~controller]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // vai retornar uma lista de categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()  // ActionResult, éporque posso ter mais de um tipo de retorno, um objeto, um status code
        {
            var categoriesDto = await _categoryService.GetCategories();
            if (categoriesDto is null)
            {
                return NotFound("Categories not found");  // retorna um 404
            }
            return Ok(categoriesDto);  // OK é um objeto result, retorna um status 200 e retorna as categorias
        }

        // vai retornar todos os categorias com os produtos
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriasProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesProducts();
            if (categoriesDto == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categoriesDto);
        }

        // retornar uma categoria pelo id
        [HttpGet("{id:int}", Name = "GetCategory")]  //  Name = "GetCategory", definiu um nome para usar na proxima operação de incluir uma nova categoria
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if (categoryDto == null)
            {
                return NotFound("Category not found");
            }
            return Ok(categoryDto);
        }

        // incluir uma nova categoria
        // Post([FromBody] esse Post poderia dar qualquer nome
        // FromBody significa qwue esta passando no body do request os dados da categoria que esta incluindo
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Invalid Data");

            await _categoryService.AddCategory(categoryDto);

            // GetCategory, esta sendo invocado do metodo acima, pra ele retornar a categoria que foi incluido com o id da categoria
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId },
                categoryDto);
        }

        // atualizar uma categoria
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)  // verificar se o id que esta passado é igual ao id que esta no objeto category
                return BadRequest();

            if (categoryDto == null)  // categoryDto is null
                return BadRequest();

            await _categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }

        // deletar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if (categoryDto == null)
            {
                return NotFound("Category not found");
            }

            await _categoryService.RemoveCategory(id);

            return Ok(categoryDto);
        }
    }


}

/*  
        POST => /categories - Status Code = 201 Created
        GET => /categories/{id:int} - Status Code = 200 OK, 404 Not Found
        PUT => /categories/{id:int} Status Code = 200 OK, 404 Not Found, 400 Bad Request
        DELETE => /categories/{id:int} - Status Code = 204 No Content (200 OK)
*/
