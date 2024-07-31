using Microsoft.AspNetCore.Mvc;
using TestTask.BusinessLogicLayer.DTOs;
using TestTask.BusinessLogicLayer.Exceptions;
using TestTask.BusinessLogicLayer.Interfaces;
using TestTask.PresentationLayer.DTOs;

namespace TestTask.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }


        /// <summary>
        /// Получение результата JOIN-запроса с поддержкой пагинации
        /// </summary>
        /// <param name="page">Номер возвращаемой страницы</param>
        /// <param name="categoryName">Имя категории, используемое при поиске</param>
        /// <remarks>
        /// Примерный вид результата:
        /// 
        ///     [
        ///         {
        ///             "id": 4,
        ///             "name": "product",
        ///             "categoryName": "Компьютеры",
        ///             "price": 1234
        ///         },
        ///         {
        ///             "id": 5,
        ///             "name": "product",
        ///             "categoryName": "Компьютеры",
        ///             "price": 1234
        ///         },
        ///         {
        ///             "id": 6,
        ///             "name": "product",
        ///             "categoryName": "Компьютеры",
        ///             "price": 1234
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Возвращает результат запроса</response>
        /// <response code="404">Страница не найдена</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductJoinDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts([FromQuery]string? categoryName, int? page=1)
        {
            if (page <= 0)
            {
                return NotFound();
            }

            IEnumerable<ProductJoinDTO> products = await productService.GetProducts((int)page!);

            if (categoryName is not null)
            {
                products = products.Where(x => x.CategoryName == categoryName);
            }

            return Ok(products);
        }

        /// <summary>
        /// Возвращает продукт с заданным ID
        /// </summary>
        /// <param name="id">ID продукта</param>
        /// <response code="200">Возвращает информацию</response>
        /// <response code="404">Продукт с заданным ID не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductJoinDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                ProductJoinDTO product = await productService.GetProduct(id);
                return Ok(product);
            }
            catch (UnknownEntityException ex)
            {
                return NotFound(new ErrorDTO { Error = ex.Message });
            }
        }

        /// <summary>
        /// Создаёт новый продукт
        /// </summary>
        /// <param name="dto">Информация о создаваемой записи продукта</param>
        /// <remarks>
        /// Пример запроса: 
        /// 
        ///     POST /api/Products
        ///     {
        ///         "name":"Продукт 1",
        ///         "categoryId": 1,
        ///         "price": 12345
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Запись создана</response>
        /// <response code="400">Неправильный запрос</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct(ProductEditDTO dto)
        {
            try
            {
                await productService.CreateProduct(dto);
                return Ok();
            }
            catch (UnknownEntityException ex)
            {
                return BadRequest(new ErrorDTO { Error = ex.Message });
            }
        }

        /// <summary>
        /// Обновляет существующий продукт
        /// </summary>
        /// <param name="id">ID продукта</param>
        /// <param name="dto">Обновлённые данные продукта</param>
        /// <remarks>
        /// Пример запроса: 
        /// 
        ///     PUT /api/Products
        ///     {
        ///         "name":"Продукт 1",
        ///         "categoryId": 1,
        ///         "price": 12345
        ///     }
        /// </remarks>
        /// <response code="200">Продукт обновлён</response>
        /// <response code="400">Обновляемый продукт не найден</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductEditDTO dto)
        {
            try
            {
                await productService.UpdateProduct(id, dto);
                return Ok();
            }
            catch (UnknownEntityException ex)
            {
                return BadRequest(new ErrorDTO { Error = ex.Message });
            }
        }


        /// <summary>
        /// Удаляет продукт с заданным ID
        /// </summary>
        /// <param name="id">ID продукта</param>
        /// <response code="200">Продукт удалён</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productService.DeleteProduct(id);
            return Ok();
        }
    }
}
